using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;


public class ChatInputController : MonoBehaviour
{
    struct Chat 
    {
        public int chatType;
        public string msg;
    }

    TMP_InputField _text;
    GameObject _chatContent;
    List<Chat> chat = new List<Chat>();
    TMP_InputField _input;
    int _maxSize = 50;
    TMP_Dropdown dropdown;
    void Start()
    {
        _text = GetComponent<TMP_InputField>();
        _chatContent = GameObject.FindGameObjectWithTag("ChatContent");
        _input = GetComponent<TMP_InputField>();
        dropdown = transform.parent.transform.GetChild(3).GetComponent<TMP_Dropdown>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            _input.ActivateInputField();
            int selectedIndex = dropdown.value;
            string chatting = _text.text;

            if (chatting.Trim().Length == 0)
                return;
            string username = Managers.Data.PlayerController.GetUserName();
            // TODO ±”º”∏ª
            if (chatting.StartsWith("/w"))
            {
                // /w dummyClient1 æ»≥Á«œººø‰
                string[] wisperSpllit = chatting.Split(" ");
                if (wisperSpllit.Length != 3)
                {
                    _text.text = "";
                    return;
                }

                string chattingMsg = wisperSpllit[2];
                string recvName = wisperSpllit[1];

                chatting = $"{username}¥‘¿« ±”º”∏ª:{chattingMsg}";
                string myChatting = $"{recvName}¥‘ø°∞‘ ±”º”∏ª:{chattingMsg}";
                byte[] bytes = new byte[1000];
                MemoryStream ms = new MemoryStream(bytes);
                ms.Position = 0;
                byte[] recvNameBytes = Encoding.Unicode.GetBytes(recvName);
                int recvNameLength = recvNameBytes.Length;      

                byte[] chattingBytes = Encoding.Unicode.GetBytes(chatting.Trim());
                int msgSize = chattingBytes.Length;
                int pktSize = recvNameLength + msgSize + 4 + 4 + 4;

                BinaryWriter bw = new BinaryWriter(ms);
                bw.Write((Int16)Type.PacketProtocol.C2S_PLAYERWHISPER);
                bw.Write((Int16)pktSize);
                bw.Write((Int32)msgSize);
                bw.Write(chattingBytes);
                bw.Write((Int32)recvNameLength);
                bw.Write(recvNameBytes);
                Managers.Data.Network.SendPacket(bytes, pktSize, 29999);
                _chatContent.GetComponent<ChatViewController>().Push(myChatting, 2);
                _text.text = "";
            }
            else
            {
                chatting = $"{username}:{chatting}";

                byte[] bytes = new byte[1000];
                MemoryStream ms = new MemoryStream(bytes);
                ms.Position = 0;

                byte[] chattingBytes = Encoding.Unicode.GetBytes(chatting.Trim());
                int msgSize = chattingBytes.Length;
                int pktSize = msgSize + 8 + 4;

                BinaryWriter bw = new BinaryWriter(ms);
                bw.Write((Int16)Type.PacketProtocol.C2S_PLAYERCHAT);
                bw.Write((Int16)pktSize);
                bw.Write((Int32)selectedIndex);
                bw.Write((Int32)msgSize);
                bw.Write(chattingBytes);
                Managers.Data.Network.SendPacket(bytes, pktSize, 0);
                _text.text = "";
            }        
        }
    }
}

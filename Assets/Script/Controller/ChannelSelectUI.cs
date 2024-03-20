using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class ChannelSelectUI : MonoBehaviour
{
    TMP_Dropdown options;

    void Start()
    {
        options = GetComponent<TMP_Dropdown>();
        options.ClearOptions();
        Init();
    }

    public void Init()
    {
        int nowch = Managers.Data.channel;
        Type.ServerType type = Managers.Data.serverType;
        int maxChannelCnt = Managers.Data.channelMaxList[(int)type];
        string channelName = GetChannelName(Managers.Data.serverType);
        List<string> optionList = new List<string>();

        for (int i = 0; i < maxChannelCnt; i++)
            optionList.Add($"{GetChannelName(type)} {i + 1}");

        options.AddOptions(optionList);
        options.value = (int)nowch; 
        options.onValueChanged.AddListener(delegate { setDropDown(options.value); });
    }

    private string GetChannelName(Type.ServerType type) 
    {
        switch (type) 
        {
            case Type.ServerType.VILLAGE:
                return "마을";
            case Type.ServerType.NOVICE:
                return "동쪽 숲";
            case Type.ServerType.INTERMEDIATE:
                return "폐허된 마을";
            case Type.ServerType.HIGH:
                return "저주받은 땅";
        }
        return null;
    }

    void setDropDown(int option)
    {
        // option 관련 동작
        Debug.Log("current option : " + option);
        Managers.Data.Clear();
        Managers.Data.Network.ServerDisConnect();
        int userSQ = Managers.Data.userSQ;
        int playerSQ = Managers.Data.playerSQ;
        byte[] bytes = new byte[1000];
        MemoryStream ms = new MemoryStream(bytes);
        ms.Position = 0;
        int pktSize = 20;
        BinaryWriter bw = new BinaryWriter(ms);
        bw.Write((Int16)Type.PacketProtocol.C2S_SERVER_MOVE);
        bw.Write((Int16)pktSize);
        bw.Write(userSQ);
        bw.Write(playerSQ);
        int serverType = (int)Managers.Data.serverType;
        int channel = option;
        bw.Write((Int32)serverType);
        bw.Write((Int32)channel);
        Managers.Data.Network.SendPacket(bytes, pktSize, 29999);
    }
}

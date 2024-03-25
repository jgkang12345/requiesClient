using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class SceneManagement : MonoBehaviour
{
    void Awake()
    {
        GameObject networkGameObject = new GameObject();
        networkGameObject.name = "@Network";
        Network network = networkGameObject.AddComponent<Network>();
        DontDestroyOnLoad(network);

        Managers.Data.Network = network.GetComponent<Network>();
        DontDestroyOnLoad(gameObject);
        Init("LoginScene");
    }

    public void Init(string scene)
    {
        switch (scene)
        {
            case "LoginScene":
                LoginInit();
                break;

            case "SelectCharacterScene":
                SelectCharacterInit();
                break;

            case "NoviceFieldScene":
                NoviceFieldInit();
                break;

            case "VillageScene":
                VillageInit();
                break;

            case "IntermediateFieldScene":
                IntermediateFieldInit();
                break;

            case "HighScene":
                HighSceneInit();
                break;
        }
    }

    private void HighSceneInit() 
    {
        Managers.Data.Network.ServerConnect(Managers.Data.port);
        GameObject playerUi = Managers.Resource.Instantiate("UI/PlayerUI");
        playerUi.name = "playerUI";
        byte[] bytes = new byte[1000];
        MemoryStream ms = new MemoryStream(bytes);
        ms.Position = 0;
        BinaryWriter bw = new BinaryWriter(ms);
        bw.Write((Int16)Type.PacketProtocol.C2S_PLAYERINIT);
        bw.Write((Int16)28);
        bw.Write((Int32)Managers.Data.userSQ);
        bw.Write((Int32)Managers.Data.playerSQ);
        bw.Write((float)Managers.Data.channelMoveMyPos.x);
        bw.Write((float)Managers.Data.channelMoveMyPos.y);
        bw.Write((float)Managers.Data.channelMoveMyPos.z);
        bw.Write((Int32)Managers.Data.prevServerType);
        Managers.Data.Network.SendPacket(bytes, 28, 0);
        Managers.Data.channelMoveMyPos = new Vector3(-1, -1, -1);
        Managers.Data.prevServerType = Type.ServerType.HIGH;

    }

    private void IntermediateFieldInit()
    {
        Managers.Data.Network.ServerConnect(Managers.Data.port);

        GameObject playerUi = Managers.Resource.Instantiate("UI/PlayerUI");
        playerUi.name = "playerUI";
        byte[] bytes = new byte[1000];
        MemoryStream ms = new MemoryStream(bytes);
        ms.Position = 0;
        BinaryWriter bw = new BinaryWriter(ms);
        bw.Write((Int16)Type.PacketProtocol.C2S_PLAYERINIT);
        bw.Write((Int16)28);
        bw.Write((Int32)Managers.Data.userSQ);
        bw.Write((Int32)Managers.Data.playerSQ);
        bw.Write((float)Managers.Data.channelMoveMyPos.x);
        bw.Write((float)Managers.Data.channelMoveMyPos.y);
        bw.Write((float)Managers.Data.channelMoveMyPos.z);
        bw.Write((Int32)Managers.Data.prevServerType);
        Managers.Data.Network.SendPacket(bytes, 28, 0);
        Managers.Data.channelMoveMyPos = new Vector3(-1, -1, -1);
        Managers.Data.prevServerType = Type.ServerType.INTERMEDIATE;
    }

    private void VillageInit()
    {
        Managers.Data.Network.ServerConnect(Managers.Data.port);

        GameObject playerUi = Managers.Resource.Instantiate("UI/PlayerUI");
        playerUi.name = "playerUI";
        byte[] bytes = new byte[1000];
        MemoryStream ms = new MemoryStream(bytes);
        ms.Position = 0;
        BinaryWriter bw = new BinaryWriter(ms);
        bw.Write((Int16)Type.PacketProtocol.C2S_PLAYERINIT);
        bw.Write((Int16)28);
        bw.Write((Int32)Managers.Data.userSQ);
        bw.Write((Int32)Managers.Data.playerSQ);
        bw.Write((float)Managers.Data.channelMoveMyPos.x);
        bw.Write((float)Managers.Data.channelMoveMyPos.y);
        bw.Write((float)Managers.Data.channelMoveMyPos.z);
        bw.Write((Int32)Managers.Data.prevServerType);
        Managers.Data.Network.SendPacket(bytes, 28, 0);
        Managers.Data.channelMoveMyPos = new Vector3(-1, -1, -1);
        Managers.Data.prevServerType = Type.ServerType.VILLAGE;
    }

    private void NoviceFieldInit()
    {
        Managers.Data.Network.ServerConnect(Managers.Data.port);

        GameObject playerUi = Managers.Resource.Instantiate("UI/PlayerUI");
        playerUi.name = "playerUI";
        byte[] bytes = new byte[1000];
        MemoryStream ms = new MemoryStream(bytes);
        ms.Position = 0;
        BinaryWriter bw = new BinaryWriter(ms);
        bw.Write((Int16)Type.PacketProtocol.C2S_PLAYERINIT);
        bw.Write((Int16)28);
        bw.Write((Int32)Managers.Data.userSQ);
        bw.Write((Int32)Managers.Data.playerSQ);
        bw.Write((float)Managers.Data.channelMoveMyPos.x);
        bw.Write((float)Managers.Data.channelMoveMyPos.y);
        bw.Write((float)Managers.Data.channelMoveMyPos.z);
        bw.Write((Int32)Managers.Data.prevServerType);
        Managers.Data.Network.SendPacket(bytes, 28, 0);
        Managers.Data.channelMoveMyPos = new Vector3(-1, -1, -1);
        Managers.Data.prevServerType = Type.ServerType.NOVICE;
    }

    private void SelectCharacterInit()
    {
        byte[] bytes = new byte[1000];
        MemoryStream ms = new MemoryStream(bytes);
        ms.Position = 0;

        BinaryWriter bw = new BinaryWriter(ms);
        bw.Write((Int16)Type.PacketProtocol.C2S_CHARACTERLIST);
        bw.Write((Int16)8);
        bw.Write((Int32)Managers.Data.userSQ);
        Managers.Data.Network.SendPacket(bytes, 8, 29999);
        Managers.Data.prevServerType = Type.ServerType.NOVICE;
    }

    private void LoginInit()
    {

    }
}

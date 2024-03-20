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
        bw.Write((Int16)12);
        bw.Write((Int32)Managers.Data.userSQ);
        bw.Write((Int32)Managers.Data.playerSQ);
        Managers.Data.Network.SendPacket(bytes, 12, 0);

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
        bw.Write((Int16)12);
        bw.Write((Int32)Managers.Data.userSQ);
        bw.Write((Int32)Managers.Data.playerSQ);
        Managers.Data.Network.SendPacket(bytes, 12, 0);
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
        bw.Write((Int16)12);
        bw.Write((Int32)Managers.Data.userSQ);
        bw.Write((Int32)Managers.Data.playerSQ);
        Managers.Data.Network.SendPacket(bytes, 12, 0);
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
        bw.Write((Int16)12);
        bw.Write((Int32)Managers.Data.userSQ);
        bw.Write((Int32)Managers.Data.playerSQ);
        Managers.Data.Network.SendPacket(bytes, 12, 0);
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
    }

    private void LoginInit()
    {

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PotalController : MonoBehaviour
{
    [SerializeField]
    private Type.ServerPort go;

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != Managers.Data.PlayerController.gameObject) return;

        if (go == Type.ServerPort.NOVICE_PORT)
            GoField(Type.ServerType.NOVICE, 0);
        else if (go == Type.ServerPort.VILLAGE_PORT)
            GoField(Type.ServerType.VILLAGE, 0);
        else if (go == Type.ServerPort.INTERMEDIATE_PORT)
            GoField(Type.ServerType.INTERMEDIATE, 0);
        else if (go == Type.ServerPort.HIGH_PORT)
            GoField(Type.ServerType.HIGH, 0);
    }

    private void GoField(Type.ServerType moveServer, int movechannel) 
    {
        Managers.Data.BackupData = new Type.PlayerInfoBackUp();
        Managers.Data.BackupData.exp = Managers.Data.PlayerController.GetExp();
        Managers.Data.BackupData.hp = Managers.Data.PlayerController.GetHp();
        Managers.Data.BackupData.mp = Managers.Data.PlayerController.GetMp();
        Managers.Data.BackupData.hpMax = Managers.Data.PlayerController.GetHpMax();
        Managers.Data.BackupData.mpMax = Managers.Data.PlayerController.GetMpMax();

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
        bw.Write((Int32)moveServer);
        bw.Write((Int32)movechannel);
        Managers.Data.Network.SendPacket(bytes, pktSize, 29999);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public class Network : MonoBehaviour
{
    private Dictionary<int, TCPConnector> _connectors = new Dictionary<int, TCPConnector>();
    private PacketHandler _packetHandler = new PacketHandler();
    private const int _recvBufferSize = 4096 * 10;
    private byte[] _recvBuffer = new byte[_recvBufferSize];
    public int NowPort;
    private static Dictionary<int, bool> _threadFlags = new Dictionary<int, bool>();

    public string LocalIp
    {
        get
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }

    void Awake()
    {
        Init();
    }

    public void ServerConnect(int port)
    {
        if (!_connectors.ContainsKey(port))
            _connectors.Add(port, new TCPConnector());

        if (_connectors[port].ConnectTo(Type.IP, (int)port))    
        {
            NowPort = port;
            Thread recvT = new Thread(new ThreadStart(() => TCPRecvProc(port)));
            recvT.Start();
        }
    }

    public void ServerDisConnect() 
    {
        _threadFlags[NowPort] = false;
        _connectors[NowPort].DisConnectTo();
    }

    void Init()
    {
        ServerConnect(30003);
    }

    public void SendPacket(byte[] buffer, int sendSize, int port)
    {
        try
        {
            if (port != 30003 && port != 29999)
                port = NowPort;
            
            if (_connectors[port].ConnectSocket.Connected)
                _connectors[port].ConnectSocket.Send(buffer, sendSize, SocketFlags.None);
        }
        catch (Exception e) 
        {
            Debug.Log($"Port:{port} {e}" );
        }
    }

    void Update()
    {
        while (true)
        {
            ArraySegment<byte> packet = PacketQueue.Instance.Pop();

            if (packet == null)
                break;

            if (packet != null)
            {
                _packetHandler.Handler(packet);
            }
        }
    }
    
    private void TCPRecvProc(int port)
    {
        _threadFlags[port] = true;

        int recvSize = 0;
        int readPos = 0;
        int writePos = 0;
        try
        {
            while (_threadFlags[port])
            {
                recvSize = _connectors[port].ConnectSocket.Receive(_recvBuffer, writePos, _recvBuffer.Length - writePos, SocketFlags.None);

                if (recvSize < 1)
                {
                    // _connectors[port].ConnectSocket.Close();
                    Debug.Log($"{port} Recv Thread가 종료되었음");
                    break;
                }

                writePos += recvSize;
                // [200][100][200][100]
                while (true)
                {
                    int dataSize = Math.Abs(writePos - readPos);

                    if (dataSize < 4) break;

                    ArraySegment<byte> pktCodeByte = new ArraySegment<byte>(_recvBuffer, readPos, readPos + sizeof(UInt16));
                    ArraySegment<byte> pktSizeByte = new ArraySegment<byte>(_recvBuffer, readPos + sizeof(UInt16), readPos + sizeof(UInt16));

                    Int16 pktCode = BitConverter.ToInt16(pktCodeByte);
                    Int16 pktSize = BitConverter.ToInt16(pktSizeByte);

                    if (pktSize > dataSize)
                        break;

                    ArraySegment<byte> segment = new ArraySegment<byte>(_recvBuffer, readPos, pktSize);
                    byte[] data = new byte[pktSize];

                    Array.Copy(segment.ToArray(), data, pktSize);

                    PacketQueue.Instance.Push(data);

                    // TODO 데이터 처리
                    readPos += pktSize;

                    if (readPos == writePos)
                    {
                        readPos = 0;
                        writePos = 0;
                    }
                    else if (writePos >= 4096 * 4)
                    {
                        Buffer.BlockCopy(_recvBuffer, readPos, _recvBuffer, 0, dataSize);
                        writePos = dataSize;
                    }

                }
            }
        }
        catch (Exception e)
        {
          Debug.LogException(e);
          Debug.Log($"{port} Err Recv Thread가 종료되었음");
        }
    }
}

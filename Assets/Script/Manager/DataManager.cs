using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    PlayerController _playerController = null;
    Dictionary<int, PlayController>  _playDic = new Dictionary<int, PlayController>();  
    Dictionary<int, GameObject>  _monsterDic = new Dictionary<int, GameObject>();  
    public Network Network { get; set; }
    public int userSQ { get; set; }
    public int playerSQ { get; set; }
    public int port { get; set; }
    public Type.ServerType serverType { get; set; }
    public Type.ServerType prevServerType { get; set; } = Type.ServerType.NOVICE;
    public int channel { get; set; }
    public PlayerController PlayerController { get { return _playerController; } set { _playerController = value; } }
    public Dictionary<int, PlayController> PlayerDic { get { return _playDic; } }
    public Dictionary<int, GameObject> MonsterDic { get { return _monsterDic; } }
    public List<int> channelMaxList = new List<int>();

    public Type.PlayerInfoBackUp BackupData { get; set; } = null;
    public Vector3 channelMoveMyPos { get; set; } = new Vector3(-1, -1, -1);
    public void Clear() 
    {
        Managers.Resource.Destory(PlayerController.gameObject);
        PlayerController = null;
        PlayerDic.Clear();
        MonsterDic.Clear();
    }
}

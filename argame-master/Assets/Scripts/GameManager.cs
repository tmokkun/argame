using UnityEngine;
using System.Collections;

public class GameManager  {

    private static GameManager _instance;


    //不是方法 是一个对象
    public static GameManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new GameManager();
            }
            return _instance;
        }
    }

    //about the BattleInfo ,the calculate of BattleInfo is done in gamemanager
    public bool isGameOver = false;
    public int curKillCount = 0;
    public int totalEnemy = 0;
    public int stat=3;

    public void OnKillEnemy()
    {
        if (isGameOver)
        {
            return;
        }
        curKillCount++;
        if (curKillCount == totalEnemy&&totalEnemy!=0)
        {
            isGameOver = true;
            Messager.Broadcast<bool>(GameEvent.GameOver, true);
        }
        Messager.Broadcast<int>(GameEvent.KillEnemy, curKillCount);
    }

    //init for BattleInfo
    public void InitBattleInfo(int _totalEnemy)
    {
        totalEnemy = _totalEnemy;
        isGameOver = false;
        curKillCount = 0;

    }


    public CharacterType CharacterType
    {
        get
        {
            return (CharacterType)PlayerPrefs.GetInt(GameDefine.playerRole);
        }
    }

    public string PlayerName
    {
        get
        {
            return PlayerPrefs.GetString(GameDefine.playerName);
        }
    }

    public void LoadScene(string sceneName)
    {
        Application.LoadLevel(sceneName);
    }

    public T LoadResources<T>(string path) where T : Object
    {
        Object obj = Resources.Load(path);
        if(obj == null)
        {
            return null;
        }
        return (T)obj;
    }
	
}

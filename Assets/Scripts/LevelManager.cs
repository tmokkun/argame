using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    private Player player;
    public Vector3[] spawnPoints;//points to spawn enenmys

	void Start () {
        CharacterType type = GameManager.Instance.CharacterType;
        string playerResName = string.Empty;
        if(type == CharacterType.warrior)
        {
            playerResName = GameDefine.warriorPath;
        }
        else
        {
            playerResName = GameDefine.archerPath;
        }

        player = EntityManager.Instance.CreateEntity<Player>(playerResName, new Vector3(-18, 0.5f, -3), Vector3.zero);

        SpawnEnemies();


	}


    //use CreateEnemy with enenmy's paramter  
    private void SpawnEnemies()
    {

        Random.seed = System.DateTime.Now.Millisecond;
        int enemyCount = spawnPoints.Length;//the soure of Battleinfo for enenmy
        for(int i = 0; i < enemyCount; i++)
        {

            Vector3 dir = new Vector3(Random.Range(0, 360), 0, Random.Range(0, 360));
           Enemy enemy= EntityManager.Instance.CreateEnemy(GameDefine.enemyPath, spawnPoints[i], dir);
            //  enemy.InitEnemy(spawnPoints, player.mRoot);//init data for ennemy 
            enemy.InitEnemy(spawnPoints, player);//init data for ennemy   ,parameter type has been changed to player not entity

        }
        GameManager.Instance.InitBattleInfo(enemyCount); //init for relative Battleinfo for enenmy
    }

	// Update is called once per frame
	void Update () {
	


	}
}

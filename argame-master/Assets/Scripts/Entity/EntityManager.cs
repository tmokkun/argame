using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EntityManager  {
    private static EntityManager _Instance;
    public static EntityManager Instance
    {
        get
        {
            if(_Instance == null)
            {
                _Instance = new EntityManager();
            }
            return _Instance;
        }
    }
    public List<Enemy>enemyList = new List<Enemy>();


    //a template to generate differrent kinds of obj 
    public T CreateEntity<T>(string entityName, Vector3 pos, Vector3 dir) where T : Entity
    {
        GameObject loadobj = GameManager.Instance.LoadResources<GameObject>(entityName);
        GameObject obj = GameObject.Instantiate(loadobj);
        //Entity entity = obj.GetComponent<Entity>();
        T entity = obj.GetComponent<T>();
        entity.InitEntity(pos, dir);
        if(entity == null)
        {
           // obj.AddComponent<Entity>();
            obj.AddComponent<T>();
        }
        return (T)entity;
    }
	
    //create a entity obj and add it to the list
    public Enemy CreateEnemy(string entityName,Vector3 pos,Vector3 dir)
    {
        Enemy enemy = CreateEntity<Enemy>(entityName, pos, dir);
        enemyList.Add(enemy);
        return enemy;
    }
    public void RemoveEnemy(Enemy enemy)
    {
        if (!enemyList.Contains(enemy))
        {
            return;
        }
        enemyList.Remove(enemy);
    }
}

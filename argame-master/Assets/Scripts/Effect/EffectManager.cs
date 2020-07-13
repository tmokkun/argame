using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EffectManager : MonoBehaviour {

    private static EffectManager _Instance;


    //create a static EffectManager obj 
    public static EffectManager Instance
    {
        get
        {

            //check whether obj _instance exsist 
            if (_Instance == null)
            {
                //use FindObjectOfType to try again 
                _Instance = FindObjectOfType(typeof(EffectManager)) as EffectManager;
                if (_Instance == null)
                {
                    //create _instance added with the class of effectmanager
                    GameObject obj = new GameObject();
                    _Instance = (EffectManager)obj.AddComponent<EffectManager>();
                        }

            }
            return _Instance;
        }
    }

    //create a list of effect type to manager effect
    List<Effect> effectList = new List<Effect>();

    private void Awake()
    {
        
    }

    //create a  effect and add to effcet list
    // public void CreateEffect(string resName,Vector3 pos)
    public Effect CreateEffect(string resName, Vector3 pos) 
    {
        Effect effect = new Effect(); // create a effect obj as effrct is not a mon0behavir
        effect.InitEffect(resName, pos); //init effect's transform by using InitEffect
        effectList.Add(effect); //add to effcet list
        return effect;
    }

    //create a  effect and set its parent and target and harm of effect as harm is dealed by effcet obj
    public Effect CreateEffect(string resName, Vector3 pos,Transform parent,Vector3 targetPos,int harm)
    {
        FlyEffect effect = new FlyEffect(); // create a faly effect obj (not effrct )
        effect.InitEffect(resName, pos,parent,targetPos); //init effect's transform by using InitEffect setparent and 
        effect.SetHarm(harm);
        effectList.Add(effect); //add to effcet list
        return effect;
    }

    //create a  effect and set its parent
    public Effect CreateEffect(string resName, Vector3 pos, Transform parent)
    {
        Effect effect = new Effect();
        effect.InitEffect(resName, pos, parent);
        effectList.Add(effect);
        return effect;
    }
    void Start () {
		
	}

    // decrease life time of  list's effect  and delete effect when its life is 0(means it is played enough)
    void Update () {
        if (effectList.Count > 0)
        {
            for(int i=0; i < effectList.Count; i++)
            {
                if (effectList[i] == null ||effectList[i].lifeTime==GameDefine.skillFXForeverLife ) // if life=-100 directly goto next effect element
                {
                    continue;
                }
                effectList[i].lifeTime -= Time.deltaTime;
                if (effectList[i].lifeTime <= 0)
                {
                    //destroy obj and remove from the list
                    GameObject.Destroy(effectList[i].mRoot.gameObject); // remove effect.mRoot (the model)
                    effectList.Remove(effectList[i]); //remove effect(the element of list)
                }
            }
        }
	}
}

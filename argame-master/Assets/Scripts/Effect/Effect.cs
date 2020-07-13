using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class Effect : MonoBehaviour { // not MonoBehaviour as EffectManager.CreateEffect() use a new()
public class Effect { 
    
    protected EffectScript mScript;//child class can  use protected
    public Transform mRoot;

    public float lifeTime;


    //create  the model obj and opreate it with mRoot.
    //effectmanager.CreateEffect create a effct obj but its tranform is not mroot's tranform 
    //mRoot is   effct.mRoot  , which means mRoot obj belongs to effct obj 
    //and mRoot.transform is same with model's transform
    public void InitEffect(string resName,Vector3 pos)
    {
        // load FX (effect)file 
        GameObject loadResObj = GameManager.Instance.LoadResources<GameObject>(resName);// load FX (effext 
        //init an temporarily for FX 
        GameObject obj = GameObject.Instantiate(loadResObj);

        //get transform of game obj
        mRoot = obj.transform;

        //get EffectScript cs of  transform of obj to check avaliability
        mScript = obj.GetComponent<EffectScript>() as EffectScript;

        if (mScript == null)//nofication for null of effect
        {
            Debug.LogError("none effect");
            return;
        }
        lifeTime = mScript.lifeTime;
        mRoot.position = pos;

    }
    // if an effect obj use this function,make its tranfrom as this function load ,then set obj's parent in addtion
    public void InitEffect(string resName,Vector3 pos, Transform parent)
    {

        InitEffect(resName, pos);
         mRoot.transform.position = Vector3.zero;
        //use parameter set obj 's tranform's parent
        mRoot.SetParent(parent);

        //effect's position realtive to parent
        mRoot.transform.localPosition = Vector3.zero;
        mRoot.transform.rotation = Quaternion.identity;

    }
    // 重载 for fly effect ,only do the InitEffect(resName, pos, parent); ,the rest leave flyeffect to override
    public virtual void InitEffect(string resName, Vector3 pos, Transform parent, Vector3 targetPos)
    {
        InitEffect(resName, pos, parent);
    }
}

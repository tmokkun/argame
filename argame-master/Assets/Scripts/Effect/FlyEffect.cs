using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Holoville.HOTween;
using Holoville.HOTween.Plugins;

public class FlyEffect : Effect {

    private int harm;
    public override void InitEffect(string resName, Vector3 pos, Transform parent, Vector3 targetPos)
    {
        base.InitEffect(resName, pos, parent, targetPos);
        mScript.SetColliderCallback(OnColliderHandler);//as same as  mScript.OnColliderHandler
        //reson to create SetColliderCallback is that  SetColliderCallback is a function retun what it recive
        //it means it is a general container,and other class can just use SetColliderCallback to
        // use OnColliderHandler and so on
        Move(targetPos);
    }

    public void SetHarm(int _harm) //make harm be revalueable
    {
        harm = _harm;
    }

    private void Move(Vector3 targetPos)
    {
        //templete for HOTween
        HOTween.Init();
        TweenParms parm = new TweenParms();
        parm.Prop("position", targetPos); //start and end
        parm.Ease(EaseType.Linear); //type of move
        parm.OnComplete(OnArrive);//after arrival make life time =0 to let effectmange delete tihs
        HOTween.To(mRoot, 2, parm);//obj  to move duration time
    }

    private void OnArrive() {
        lifeTime = 0;
    }
    //generate hit effct and let enemy lose life
    private void OnColliderHandler(Transform other)
    {
        EffectManager.Instance.CreateEffect(GameDefine.archerSkillHit, mRoot.position);// create a hit effect
        Enemy enemy = other.GetComponent<Enemy>();
        enemy.GetHurt(harm);
        lifeTime = 0;
    }
}

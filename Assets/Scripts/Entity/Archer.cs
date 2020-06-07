using UnityEngine;
using System.Collections;

public class Archer : Player {

    private float attackRadius=10;
    private Quaternion curRotation;//fix a problem of anmaion

    


    protected override void OnEnterAttack1()
    {
        base.OnEnterAttack1();//archer's anime 
        curRotation = mRoot.rotation;//fix a problem of anmaion
        Invoke("PlayAttack1Effect", 0.7f);//skill effect 's anime
    }

    
    private void PlayAttack1Effect()
    {
        mRoot.rotation = curRotation;// fix a problem of anmaion
        Vector3 targetPos = mRoot.position + attackRadius * mRoot.forward.normalized;//largest range for fly effect
        EffectManager.Instance.CreateEffect(GameDefine.archerSkill1,Vector3.zero,middlePoint, targetPos,harmNumber);
    }
    protected override void OnEnterAttack2()
    {
        base.OnEnterAttack2();

        curRotation = mRoot.rotation;
        Invoke("PlayAttack2Effect", 0.8f);

    }

    private void PlayAttack2Effect()
    {

        mRoot.rotation = curRotation;// fix a problem of anmaion
        Vector3 targetPos1 = mRoot.position + attackRadius * mRoot.forward.normalized;//largest range for fly effect
        float middleAngle = Vector3.Angle(mRoot.forward, Vector3.right);//calculate the angle between midldle point and right axis
        middleAngle = mRoot.forward.z > 0 ? middleAngle : -middleAngle;//make anggle alaways be positive

        //set second and third point's x anf z coordinate base on mRoot(frist)
        float x2 = mRoot.position.x + attackRadius * Mathf.Cos((middleAngle + 30) * Mathf.Deg2Rad);
        float z2 = mRoot.position.z + attackRadius * Mathf.Sin((middleAngle + 30) * Mathf.Deg2Rad);
        float x3 = mRoot.position.x + attackRadius * Mathf.Cos((middleAngle - 30) * Mathf.Deg2Rad);
        float z3 = mRoot.position.z + attackRadius * Mathf.Cos((middleAngle - 30) * Mathf.Deg2Rad);

        Vector3 targetPos2 = new Vector3(x2, targetPos1.y, z2);
        Vector3 targetPos3 = new Vector3(x3, targetPos1.y, z3);

        //build thiree CreateEffect with three diferrent paramters for HOTween
        EffectManager.Instance.CreateEffect(GameDefine.archerSkill2, Vector3.zero, middlePoint, targetPos1,harmNumber);
        EffectManager.Instance.CreateEffect(GameDefine.archerSkill2, Vector3.zero, middlePoint, targetPos2, harmNumber);
        EffectManager.Instance.CreateEffect(GameDefine.archerSkill2, Vector3.zero, middlePoint, targetPos3, harmNumber);
    }

}

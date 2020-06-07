using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Warrior : Player {

    private float attackRadius = 2;
    protected override void OnEnterAttack1()
    {
        //execute father's code
        base.OnEnterAttack1();
        //code for warroir
        //delay the animation for 1s
        Invoke("PlayerAttack1Effect", 0.7f);

        //create warroir's effcet using path of warroir's effcet's model and tranform with cs of warroir
        // EffectManager.Instance.CreateEffect(GameDefine.warriorSkill1,mRoot.position,middlePoint);

    }

    private void PlayerAttack1Effect()
    {

        CalculateHarm(AttackType.Normal);
        //create warroir's effcet using path of warroir's effcet's model and tranform with cs of warroir
        EffectManager.Instance.CreateEffect(GameDefine.warriorSkill1, Vector3.zero, middlePoint);

    }

    //play both charactor's animation and skill's animation 
    protected override void OnEnterAttack2()
    {
        base.OnEnterAttack2(); //play  charactor's animation
        Invoke("PlayAttack2Effect", 0.7f);//play skill's animation 

    }

    //GameDefine.warriorSkill2 store path name  CreateEffect uses InitEffect,and InitEffect use path name to
    //load FX file 
    private void PlayAttack2Effect()
    {
        CalculateHarm(AttackType.Skill);
        EffectManager.Instance.CreateEffect(GameDefine.warriorSkill2, Vector3.zero, bottomPoint);

    }

    private void CalculateHarm(AttackType type)
    {
        List<Enemy> enemyList = EntityManager.Instance.enemyList; //no need to bulid a EntityManager as Instance
        for (int i = 0; i < enemyList.Count; i++) // calculate every enemy
        {
            if (enemyList[i] == null) //if an enemy iss null calculate the next enemy
            {
                continue;
            }
            float dis = Vector3.Distance(mRoot.position, enemyList[i].mRoot.position);
            if (dis < attackRadius)
            {
                if (type == AttackType.Normal) // for the nomarl attack the range is a half circle in player's direct
                {
                    Vector3 forward = transform.forward.normalized; // vector for player 's direct
                    Vector3 toOther = enemyList[i].mRoot.position - mRoot.position; //vector for (enemy's position  - player's position)
                    if (Vector3.Dot(forward, toOther) > 0) // calculate dot
                    {
                        enemyList[i].GetHurt(harmNumber); //this harm number is belong to warrior
                    }

                }
                else if (type == AttackType.Skill)
                {
                    enemyList[i].GetHurt(harmNumber);
                }
            }
        }
    }
}

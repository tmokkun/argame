using UnityEngine;
using System.Collections;

public class Enemy : Entity{
    private Vector3[] movePoints;
    private float stayTime = 0;
    private Transform target;
    private float chaseRadius = 3;
    private float attackRadius = 1.5f;
    private Player targetEntity;
    private float attackCoolTime = 1;
    private float curCoolTime = 0;


    public override void InitEntity(Vector3 pos, Vector3 dir)
    {
        //如果说你父类里面有一个成员比如int  a;那么你子类里面也可以再定义一个int a，这个时候base.a和this.a表示的就不是一个变量了，如果说子类里面没有，那么base.a和this.a表示的都是一个变量。
        base.InitEntity(pos, dir);
        totalLife = Random.Range(30, 50); //use Entity 's attrbute
        curLife = totalLife;
        harmNumber = 1;
        curCoolTime = attackCoolTime;
    }
    protected override void OnEnterAttack1()
    {
        base.OnEnterAttack1();
        curCoolTime = 0;
        Invoke("CalculateHarm", 0.7f); 
    }

    //judge  wether do harm ,if so ,do the harm
    private void CalculateHarm()
    {
        if (target == null)
        {
            return;
        }
        float dis = Vector3.Distance(mRoot.position, target.position);
        float targetAngle = Vector3.Angle(mRoot.forward.normalized, (target.position - mRoot.position).normalized);//calculate the angle between player and enemy
        if (dis < attackRadius && targetAngle < 30)
        {
            targetEntity.GetHurt(harmNumber);//gethurt is a entity cs it can both use in player and enermy
        }
        OnEnterIdle();
    }


    //public void InitEnemy(Vector3[] points, Transform player)
    public void InitEnemy(Vector3[] points, Player player)
    {
       // totalLife = Random.Range(30, 50); //use Entity 's attrbute
       // curLife = totalLife;
        movePoints = points;
        // target = player; //use in update to give enemy the target to chase
        target = player.mRoot; //taget change to tranform type to easy to use position
        targetEntity = player;  //gethurt is used in entity type
        stayTime = Random.Range(0, 10);
    }

        private void Update()
        {
        if(GameManager.Instance .isGameOver)
        {
            if (mState == EntityState.Run) //when play die also nned to stop
            {
                mNav.Stop();
                OnEnterIdle();
                
            }
            return; //need to add this or the remain of Update code will be executed
        }

        if (curCoolTime < attackCoolTime)
        {
            curCoolTime += Time.deltaTime;

        }
        if(movePoints==null
            ||movePoints.Length==0
            ||mState==EntityState.Death
            ||mState==EntityState.Hit
            || mState == EntityState.Attack1)// do nothing this frame if no enemy in list or enemy is die attack and so on 
        {
            return;
        }


        //if (movePoints == null || movePoints.Length == 0)//no enemy in list 
        //    {
        //        return;
        //    }

            //decide whether chase player
            if (target != null&&mState!=EntityState.Death&&mState!=EntityState.Hit) {
            float targetDis = Vector3.Distance(mRoot.position,target.position);//mroot is this.tranform according to mroot 's define 

            //when chasing if in the range and not in cooldown
            if (targetDis <= attackRadius&&curCoolTime>=attackCoolTime)  
            {
                startAttack();
                return; //return is important  as each frame can only do one act
            }


            if (targetDis <= chaseRadius&&targetDis>attackRadius) // play is into enenmy 's chase radius
            {
                StarChase();
                    return;
            }

        }
        // from stay to move
           if (stayTime > 0)
        {
            stayTime -= Time.deltaTime;
            return;
        }
        // use a tag to avoid using move() when moving
        if (mState == EntityState.Idle && stayTime <= 0) {
            StartMove();
            return;
        }


        // from move to stay
        if (mState == EntityState.Run && mNav.remainingDistance <= mNav.stoppingDistance)
            StartIdle();
        }
    private void Move(Vector3 targetPos)
    {
        mNav.Resume();// reset mNav
        OnEnterRun(); //paly animation and tag
        mRoot.LookAt(targetPos); // set direction
        mNav.SetDestination(targetPos); //init mNav
    }
    private void StartIdle() {
        stayTime = Random.Range(2, 20); //generate a random time to stay
        mNav.Stop(); // stop mNav
        OnEnterIdle();
    }
    private void StarChase()
    {
        Vector3 tarpos = target.position;
        Move(tarpos);
    }
    private void startAttack()
    {
        mRoot.LookAt(target);
        OnEnterAttack1();

    }

    //add kill record adn list management for OnEnterDeath
    protected override void OnEnterDeath()
    {
        base.OnEnterDeath();
        EntityManager.Instance.RemoveEnemy(this);// list is used to judege info so need to update
        GameManager.Instance.OnKillEnemy(); //record kill

    }


    private void StartMove() //choose a random spot and use move()
    {
        Vector3 targetPos = movePoints[Random.Range(0, movePoints.Length - 1)]; //choose a random spot in list to move
        float dis = Vector3.Distance(targetPos, mRoot.position);
        if (dis > 2) //if <2 means target is its present position and no need to move
        {
            Move(targetPos);
        }
    }

}
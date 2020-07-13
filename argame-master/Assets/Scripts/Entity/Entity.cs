using UnityEngine;
using System.Collections;

public enum AttackType
{
    Normal,
    Skill,
}

public enum EntityState
{
    Idle,
    Run,
    Hit,
    Attack1,
    Attack2,
    Death,
}

public class Entity : MonoBehaviour {

    protected Animator animator;
    public Transform mRoot;
    public EntityState mState;
    public UnityEngine.AI.NavMeshAgent mNav;
    protected Transform middlePoint;
    protected Transform bottomPoint;
    protected Transform topPoint;

    protected int totalLife;
    protected int curLife;
    protected int harmNumber;
    public AudioClip[] entityAudios;
    protected AudioSource mAudio;
    private enum AudioIndex
    {
        attack=0,
        hurt=1,
        death=2,
    }

    protected BloodSlide plyslide;
    protected BloodSlide bloodslider;

    void Awake () {
        mRoot = transform; //tranform ==this.transform
        animator = mRoot.GetComponent<Animator>() as Animator;
        mNav = mRoot.GetComponent<UnityEngine.AI.NavMeshAgent>() as UnityEngine.AI.NavMeshAgent;


        //find point in obj's chlid obj
        topPoint = mRoot.Find("Point/Top");
        middlePoint = mRoot.Find("Point/Middle"); ;
        bottomPoint = mRoot.Find("Point/Bottom");
        mAudio = mRoot.GetComponent<AudioSource>();
    }

    public virtual void InitEntity(Vector3 pos,Vector3 dir)
    {
        mRoot.position = pos;
        mRoot.rotation = Quaternion.LookRotation(dir);
        OnEnterIdle();
        CreateBloodSlider();

    }
    protected void CreateBloodSlider()
    {
        GameObject objLoaded = GameManager.Instance.LoadResources<GameObject>(GameDefine.UI_BloodSlider);
        GameObject obj = GameObject.Instantiate(objLoaded) as GameObject;
        obj.transform.SetParent(topPoint); //add it to a model 
        obj.transform.localPosition = Vector3.zero; 
        bloodslider = obj.GetComponent<BloodSlide>(); //let bloodslider as obj's  BloodSlide cs 
        if (bloodslider == null)
        {
            bloodslider = obj.AddComponent<BloodSlide>();
        }
    }

    
    protected void RefreshBloodSlider()
    {
        bloodslider.RefreshSlider(curLife, totalLife);




        if (plyslide != null) //ply 是临时建立的 而UI是一直有的 所以可以把UI拖到player prefeb上 而不是把未建立的player prefeb 拖到UI上
        {

            plyslide.transform.GetComponent<BloodSlide>().RefreshSlider(curLife, totalLife);
        }

    }


    public void PlayAnimation(string animName)
    {
        if(animName == string.Empty)
        {
            Debug.Log("传入动作为空" + animName);
            return;
        }
        animator.Play(animName);
    }


    protected virtual void OnEnterIdle()
    {
        mState = EntityState.Idle;
        PlayAnimation(GameDefine.animIdle);
    }

    protected virtual void OnEnterRun()
    {
        mState = EntityState.Run;
        PlayAnimation(GameDefine.animRun);
    }

    protected virtual void OnEnterHit()
    {

        if (GameManager.Instance.stat ==3)
        {
            mNav.Stop(); //let attack do naturely
        }
        mAudio.clip = entityAudios[(int)AudioIndex.hurt];
        mAudio.Play();
        mState = EntityState.Hit;
        PlayAnimation(GameDefine.animHit);
        Invoke("OnEnterIdle", 1.5f); // make model back into idle state
    }

    //various functions reseponsed to the attack
    protected virtual void OnEnterAttack1()
    {
        if (GameManager.Instance.stat == 3)
        {
            mNav.Stop(); //let attack do naturely
        }
        mAudio.clip = entityAudios[(int)AudioIndex.attack];
        mAudio.Play();
        mState = EntityState.Attack1; // this is used to tag the presnt action 
        PlayAnimation(GameDefine.animAttack1);
    }

    protected virtual void OnEnterAttack2()
    {
        if (GameManager.Instance.stat == 3)
        {
            mNav.Stop(); //let attack do naturely
        }
        mAudio.clip = entityAudios[(int)AudioIndex.attack];
        mAudio.Play();
        mState = EntityState.Attack2;
        PlayAnimation(GameDefine.animAttack2);
    }

    protected virtual  void OnEnterDeath()
    {
        if (GameManager.Instance.stat == 3)
        {
            mNav.Stop(); //let attack do naturely
        }
        mAudio.clip = entityAudios[(int)AudioIndex.death]; //equals to  entityAudios[2]
        mAudio.Play();
        CancelInvoke(); //when die all  invoke stop
        mState = EntityState.Death;
        PlayAnimation(GameDefine.animDeath);
        Invoke("DestroySelf", 1.5f);
    }

    public virtual void DestroySelf()
    {
        GameObject.Destroy(mRoot.gameObject);
    }

    public void GetHurt(int hurt)
    {
        if(GameManager.Instance.isGameOver ||mState==EntityState.Death) //wehn player die (gameover) also stop
        {
            return;
        }

        curLife -= hurt;
        CreateHarmNumber(hurt);
        RefreshBloodSlider();
        if (curLife <= 0)
        {
            OnEnterDeath();
        }
        else
        {
            OnEnterHit();
        }

    }

    public void CreateHarmNumber(int number)
    {
        GameObject objLoaded = GameManager.Instance.LoadResources<GameObject>(GameDefine.UI_HarmNumber);
        GameObject obj = GameObject.Instantiate(objLoaded) as GameObject;
        obj.transform.SetParent(topPoint);
        obj.transform.localPosition = Vector3.zero;
        HarmNumber harmWord = obj.GetComponent<HarmNumber>();
        if (harmWord == null)
        {
            harmWord = obj.AddComponent<HarmNumber>();
        }
        harmWord.InitHarmNumber(number * -1); //input  something like "-1000" and transnito string


    }

	void Update () {
	
	}
}

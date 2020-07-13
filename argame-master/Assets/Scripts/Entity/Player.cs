using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;


public class Player : Entity {

    public float normalCoolTime = 1;
    public float skillCoolTime = 3;
    private MainPanel skillUI;
    public GameObject came;
    


    public override void InitEntity(Vector3 pos, Vector3 dir)
    {
        base.InitEntity(pos, dir);
        GameManager.Instance.stat = 1;
            this.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().enabled = true;
            this.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
            //came.SetActive(true);
            GameObject.Find("CameraRoot").gameObject.transform.Find("MainCamera").gameObject.GetComponent<CameraFollow>().enabled = false;
        plyslide = GameObject.Find("BloodSlider_UI").GetComponent<BloodSlide>();
        //标记
        
            //Camera.main.GetComponent<CameraFollow>().InitCamera(transform);
        
        InitSkillUI(); //use InitSkillUI to init

        totalLife = 20;
        curLife = totalLife;
        harmNumber = 10;
    }

    //find find canvas obejct existed in scene and use its InitSkillTime function
    private void InitSkillUI()
    {
        skillUI = FindObjectOfType<MainPanel>(); //find obejct existed in scene
        skillUI.InitSkillTime(normalCoolTime, skillCoolTime); 

    }

    //as Player is Entity it can add function and override Entity's function and not 
    //influent the whole structure
    protected override void OnEnterAttack1()
    {
        base.OnEnterAttack1();
        //skillUI.OnPlayerUserSkill(mState);//input parameter
    }
    protected override void OnEnterAttack2()
    {
        base.OnEnterAttack2();
        //skillUI.OnPlayerUserSkill(mState);
    }

    public float timeOut = 1.0f;
    private float timeElapsed;
    void Update()
    { 
       timeElapsed += Time.deltaTime;
        if(timeElapsed >= timeOut) {
            // Do anything
            timeElapsed = 0.0f;
    }
    else{return;}

        if(GameManager.Instance .isGameOver)  // can be lose or win
        {
            if(mState == EntityState.Run) // if win player should stop
            {
                //标记
                if (GameManager.Instance.stat == 3)
                {
                    mNav.Stop();
                }
                OnEnterIdle();
            }
            return;
        }



        if (mState == EntityState.Death) //if dead do nothing
        {
            return;
        }

        //标记
        if(Input.GetMouseButtonDown(0))
        {
            if(CheckPress(Input.mousePosition))
            {
                //让玩家移动

            }
        }

        //if (Input.GetMouseButton(1)) this is consequently click

       //if (skillUI.IsNormalOK()) 
            {
            if (GameManager.Instance.stat == 3)
            {
                mNav.Stop(); //stop walking
            }
            OnEnterAttack1(); //
        }

        //标记
        if (GameManager.Instance.stat == 3)
        {
            if (mState == EntityState.Run && mNav.remainingDistance == 0)
            {

                mNav.Stop();

                OnEnterIdle();
            }
        }


        //if (skillUI.IsSkillOK())
        {
        //    OnEnterAttack2();
        }

        //标记
        if (Input.GetKeyDown(KeyCode.Alpha3) )
        {
            GameManager.Instance.stat = 3;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Debug.Log("able");
            this.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().enabled = false;
            this.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
            GameObject.Find("CameraRoot").gameObject.transform.Find("MainCamera").gameObject.GetComponent<CameraFollow>().enabled = true;
            //came.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameManager.Instance.stat = 1;
            this.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().enabled = true;
            this.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
            //came.SetActive(true);
            GameObject.Find("CameraRoot").gameObject.transform.Find("MainCamera").gameObject.GetComponent<CameraFollow>().enabled = false;
        }
        
    }

    RaycastHit hit;
    private bool CheckPress(Vector3 inputPos)
    {
     /*
        Ray ray = Camera.main.ScreenPointToRay(inputPos);
       
        Debug.DrawLine(ray.origin, hit.point, Color.red, 2);
        if(Physics.Raycast(ray,out hit))
        {
            if(hit.collider.CompareTag("Terrain"))
            {
                Move(hit.point);
                return true;
            }
        }
    */
        return false;
    }

    private void Move(Vector3 targetPos)
    {
        //标记
        if (GameManager.Instance.stat == 3)
        {
            mNav.Resume();
            mRoot.LookAt(targetPos);

            OnEnterRun();
            mNav.SetDestination(targetPos);
        }
    }

    //use father class to add some features
    protected override void OnEnterDeath()
    {
        base.OnEnterDeath();
        GameManager.Instance.isGameOver = true;
        Messager.Broadcast<bool>(GameEvent.GameOver, false);

    }


}

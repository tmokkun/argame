using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel : MonoBehaviour {


    private Image normalImage;
    private Image skillImage;
    private Transform mRoot;
    private float normalCoolTime;
    private float skillCoolTime;
    private float curnormalCoolTime;

    private float curskilllCoolTime;
    private float originCountDown = 600;
    private float countDown = 600;
    private Text timeText;
    private Text killCountText;




    // Use this for initialization
    void Awake () {
        mRoot = transform;//let mroot get obj(canvas)'s transform 
        normalImage = mRoot.Find("Normal").GetComponent<Image>();
        skillImage = mRoot.Find("Skill").GetComponent<Image>();
        timeText = mRoot.Find("CountDown/Value").GetComponent<Text>();
        killCountText = mRoot.Find("KillCount/Value").GetComponent<Text>();

        timeText.text = "00:00";
        killCountText.text = "0";


    }

     void OnEnable()
    {
        Messager.AddListener<bool>(GameEvent.GameOver, OnGameOver);
        Messager.AddListener<int>(GameEvent.KillEnemy, OnEnemyKilled);

    }

    void OnDisable()
    {
        Messager.RemoveListener<bool>(GameEvent.GameOver, OnGameOver);
        Messager.RemoveListener<int>(GameEvent.KillEnemy, OnEnemyKilled);
    }
    private void OnEnemyKilled (int curKillNumber)
    {
        killCountText.text = curKillNumber.ToString();


    }
    private void OnGameOver(bool isWin) {
        GameObject loadedObj = GameManager.Instance.LoadResources<GameObject>(GameDefine.UI_GameOverPanel);
        GameObject gameoverPanel = GameObject.Instantiate(loadedObj);
        RectTransform gameoverRectTransform = gameoverPanel.GetComponent<RectTransform>();
        gameoverRectTransform.SetParent(mRoot.GetComponent<RectTransform>());
        gameoverRectTransform.transform.localPosition = Vector3.zero;
        gameoverRectTransform.transform.localScale = Vector3.one;

        //add gameover class for UI_gameover
        GameOverPanel panel = gameoverRectTransform.GetComponent<GameOverPanel>();
        if (panel == null)
        {
            panel = gameoverRectTransform.gameObject.AddComponent<GameOverPanel>();

        }
        panel.InitPanel(isWin, GameManager.Instance.curKillCount, (int)(originCountDown - countDown));//use InitPanel with parameter

    }


    //init structure
    public void InitSkillTime(float _normalTime,float _skillTime)
    {
        //init data
        normalCoolTime = _normalTime;
        skillCoolTime = _skillTime;
        curnormalCoolTime = normalCoolTime;
        curskilllCoolTime = skillCoolTime;
        //ui 
        normalImage.fillAmount = curnormalCoolTime/normalCoolTime;
        skillImage.fillAmount = curskilllCoolTime / skillCoolTime;

    }

    //extinguish diffrent kind of skill
    public void OnPlayerUserSkill(EntityState state)
    {
        if (state == EntityState.Attack1)
        {
            curnormalCoolTime = 0;
        }
        if (state == EntityState.Attack2)
        {
            curskilllCoolTime = 0;
        }
    }

    //used for if sentence in play 's getbutton 
    public bool IsNormalOK(){
        if (curnormalCoolTime >= normalCoolTime)
        {
            return true;
        }
        return false;
    }

    public bool IsSkillOK()
    {
        if (curskilllCoolTime >= skillCoolTime)
        {
            return true;
        }
        return false;
    }




    // Update is called once per frame
    void Update () {
        if (GameManager.Instance.isGameOver) //if game over directly return
        {
            return;
        }


        if (curnormalCoolTime <= normalCoolTime)
        {
            curnormalCoolTime += Time.deltaTime;
            normalImage.fillAmount = curnormalCoolTime / normalCoolTime;
        }
        if (curskilllCoolTime <= skillCoolTime)
        {
            curskilllCoolTime += Time.deltaTime;
            skillImage.fillAmount = curskilllCoolTime / skillCoolTime;
        }
        if(countDown > 0) //realize of countdown
        {
            countDown -= Time.deltaTime;
            countDown = countDown < 0 ? 0 : countDown;
            float minute = Mathf.Floor(countDown / 60); //seperate minute
            float secondes = countDown % 60;//seperate second
            timeText.text = minute.ToString("00") + ":" + secondes.ToString("00"); //trans into string and combine
            if (countDown <= 0) //when time up lose
            {
                GameManager.Instance.isGameOver = true;
                OnGameOver(false);
            }
        }
    }
}

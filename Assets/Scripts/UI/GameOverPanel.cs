using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour {

    private Text resultText;
    private Text killCountText;
    private Text timeText;
    private Text goldText;
    private int killcount;
    private int useTime;
    private int goldNumber;
    private Transform mRoot;

    private void Awake()
    {
        mRoot = transform;
        resultText = mRoot.Find("Result").GetComponent<Text>();
        killCountText = mRoot.Find("KillCount/Value").GetComponent<Text>(); //prefeb 那边末尾加空格也会被计算在名字中 注意末尾不要有空格
        timeText = mRoot.Find("CountDown/Value").GetComponent<Text>();
        goldText = mRoot.Find("Gold/Value").GetComponent<Text>();
    }

    //init data for Panel and start the first Coroutine
    public void InitPanel(bool isWin, int _killCount, int _useTime)
    {
        if (isWin)
        {
            resultText.text = "victory";
            resultText.color = Color.red;
        }
        else
        {
            resultText.text = "lose";
            resultText.color = Color.blue;
        }

        killcount = _killCount;
        useTime = _useTime;
        goldNumber = killcount*100;
        killCountText.text = "0";
        timeText.text = "00:00";
        goldText.text = "0";

        StartCoroutine("KillNumberCount");
    }

    //reliaze dynamic plus system
    IEnumerator KillNumberCount()
    {
        int count = 0;
        while (killcount > count) //if killcount < count yield return  will not be done
        {
            count++;
            killCountText.text = count.ToString();
            yield return new WaitForSeconds(1f);
        }
        StartCoroutine("UseTimeCount"); //start another  Coroutine  
    }
    IEnumerator UseTimeCount()
    {
        int count = 0;
        while (useTime > count) //if useTime < count yield return  will not be done
        {
            count++;
            float minute = Mathf.Floor(count / 60);
            float seconds = count % 60;
            timeText.text = minute.ToString("00") + ":" + seconds.ToString("00");
            yield return new WaitForSeconds(0.1f);
        }
        StartCoroutine("GoldCount"); //start another  Coroutine  
    }
    IEnumerator GoldCount()
    {
        int count = 0;
        while (goldNumber > count)
        {
            count++;
            goldText.text = count.ToString();
            yield return new WaitForSeconds(0.005f);
        }

       
    }

}

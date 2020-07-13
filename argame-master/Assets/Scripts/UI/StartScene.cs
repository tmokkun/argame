using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartScene : MonoBehaviour {

    private Button startButton;
	
	void Start () {
        startButton = transform.GetComponent<Button>() as Button;
        startButton.onClick.AddListener(StartClick);
        PlayerPrefs.DeleteAll(); //clear data used to debug
	}
	

	void Update () {
	
	}

    private void StartClick()
    {
         //2秒後にオブジェクトを消す
        Invoke("Load", 20);
    }
    //Invokeで使う
    void Load()
    {
     if(GameManager.Instance.PlayerName != string.Empty && GameManager.Instance.PlayerName != null)
        {
            GameManager.Instance.LoadScene(GameDefine.MainScene);
        }
        else
        {
            GameManager.Instance.LoadScene(GameDefine.MainScene);
        }
     
        Debug.Log("消えた時間" + Time.time);
    }
}

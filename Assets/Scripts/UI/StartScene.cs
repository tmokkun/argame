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
        if(GameManager.Instance.PlayerName != string.Empty && GameManager.Instance.PlayerName != null)
        {
            GameManager.Instance.LoadScene(GameDefine.MainScene);
        }
        else
        {
            GameManager.Instance.LoadScene(GameDefine.CreateCharacterScene);
        }
        
    }
}

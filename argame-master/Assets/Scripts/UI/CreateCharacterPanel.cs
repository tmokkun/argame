using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CreateCharacterPanel : MonoBehaviour {

    private Button warriorButton;
    private Button archerButton;
    private Button createButton;
    private InputField nameInput;
    private Transform mRoot;
    private GameObject warriorObj;
    private GameObject archerObj;
   


	void Awake()
    {
        mRoot = transform;
        warriorButton = mRoot.Find("WarriorButton").GetComponent<Button>() as Button;
        archerButton = mRoot.Find("ArcherButton").GetComponent<Button>() as Button;
        createButton = mRoot.Find("CreateButton").GetComponent<Button>() as Button;
        nameInput = mRoot.Find("Name/InputField").GetComponent<InputField>() as InputField;

        warriorButton.onClick.AddListener(WarriorClick);
        archerButton.onClick.AddListener(ArcherClick);
        createButton.onClick.AddListener(CreateClick);
    }
    

    private void WarriorClick()
    {
        if(warriorObj == null)
        {
            GameObject obj = GameManager.Instance.LoadResources<GameObject>(GameDefine.warriorPath);
            warriorObj = GameObject.Instantiate(obj);
            warriorObj.transform.position = Vector3.zero;
        }
        warriorObj.SetActive(true);
        if(archerObj != null && archerObj.activeInHierarchy)
        {
            archerObj.SetActive(false);
        }

        PlayerPrefs.SetInt(GameDefine.playerRole, (int)CharacterType.warrior);
    }

    private void ArcherClick()
    {
        if(archerObj == null)
        {
            GameObject obj = GameManager.Instance.LoadResources<GameObject>(GameDefine.archerPath);
            archerObj = GameObject.Instantiate(obj);
            archerObj.transform.position = Vector3.zero;
        }
        archerObj.SetActive(true);
        if(warriorObj != null && warriorObj.activeInHierarchy)
        {
            warriorObj.SetActive(false);
        }
        PlayerPrefs.SetInt(GameDefine.playerRole, (int)CharacterType.archer);
    }

    private void CreateClick()
    {
        string playerName = nameInput.text;
        PlayerPrefs.SetString(GameDefine.playerName, playerName);
        GameManager.Instance.LoadScene(GameDefine.MainScene);
    }

   
}

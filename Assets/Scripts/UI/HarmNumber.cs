using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Holoville.HOTween;
using Holoville.HOTween.Plugins;

public class HarmNumber : MonoBehaviour {

	private Text wordText;
        private void Awake()
    {
        wordText = transform.GetComponent<Text>();
    }


    public void InitHarmNumber(int number)
    {
        wordText.text = number.ToString();
        HOTween.Init();
        TweenParms parm = new TweenParms();
        float y = transform.position.y;
        parm.Prop("position", new PlugVector3Y(y + 1));
        parm.Ease(EaseType.Linear);
        parm.OnComplete(DestroySelf); //do when parm over
        HOTween.To(transform, 1, parm);

    }
    private void DestroySelf()
    {
        GameObject.Destroy(gameObject);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodSlide : MonoBehaviour {

   

    private Slider mSlider;
   
   

    private void Start()
    {
        mSlider = transform.GetComponent<Slider>();
        mSlider.value = 1; //let mSlider as slide's compoment
    }

    public void RefreshSlider(int curlife, int totallife)
    {
        float value = (float)curlife / totallife;
        mSlider.value = value; // mSlider has the virual vlaue
    }

}

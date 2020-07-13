using System.Collections;
using System.Collections.Generic;
using UnityEngine;






public class EffectScript : MonoBehaviour {


    public float lifeTime;
    public Callback<Transform> callback;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void SetColliderCallback(Callback<Transform> _callbak)
    {
        callback = _callbak; //
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) //effct hit the enenmy
        {
            callback(other.transform); //callback will have the value of _callbak accroiding to InitEffect
            //as effcetscript not has set start and update ,so OnTriggerEnter will be used later than SetColliderCallback
            //and InitEffect
        }
    }
}

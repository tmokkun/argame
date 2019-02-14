using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour {

    public Vector3 offset;
    private Transform playerBip;

	// Use this for initialization
	void Start () {
        playerBip = GameObject.FindGameObjectWithTag("Player").transform.Find("Bip01");
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = playerBip.position + offset;
	}
}

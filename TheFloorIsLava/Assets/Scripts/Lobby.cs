using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Lobby : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider col)
    {
        //check if this is the host
        if (!col.gameObject.GetComponent<NetworkIdentity>().isServer)
        {
            //you have no power here- begone!
            return;
        }

        //goto actual level
        Debug.Log("HOST READY");
    }
}

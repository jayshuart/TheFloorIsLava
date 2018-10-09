using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Lobby : NetworkBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider col)
    {
        //check if this is the host
        if(col.gameObject.GetComponent<NetworkIdentity>().netId.Value != 1)
        {
            //you have no power here- begone!
            Debug.Log(col.gameObject.GetComponent<NetworkIdentity>().netId.Value);
            return;
        }
        else
        {
            //goto actual level
            Debug.Log("HOST READY");
            Destroy(col.gameObject);
        }


    }
}

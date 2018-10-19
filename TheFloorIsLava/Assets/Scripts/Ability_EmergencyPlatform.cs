using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Ability_EmergencyPlatform : NetworkBehaviour {

    [SerializeField] private GameObject throwablePrefab;
    [SerializeField] private float throwForce;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isLocalPlayer)
        {
            //left click
            if (Input.GetMouseButtonDown(0))
            {
                //actually throw our emergency platform
                throwPlatform();
            }
        }
	}

    private void throwPlatform()
    {
        //create platform
        Vector3 instantiatePos = this.transform.position + (this.transform.forward);
        GameObject throwable = GameObject.Instantiate(throwablePrefab, instantiatePos,  this.transform.localRotation);

        //apply force to it
        Rigidbody throwRB = throwable.GetComponent<Rigidbody>();
        Vector3 throwVector = this.transform.forward * throwForce;
        throwRB.AddForce(throwVector);
    }
}

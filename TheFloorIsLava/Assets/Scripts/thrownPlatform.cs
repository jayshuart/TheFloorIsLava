using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thrownPlatform : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider col)
    {
        //check if we have collided w/ lava
        if (col.gameObject.CompareTag("Lava"))
        {
            //freeze object
            this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

            //allow collisons w/ player again
            this.gameObject.GetComponent<Collider>().enabled = false;
            this.gameObject.GetComponent<Collider>().enabled = true;
        }
    }
}

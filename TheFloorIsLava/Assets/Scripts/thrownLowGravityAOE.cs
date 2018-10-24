using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thrownLowGravityAOE : MonoBehaviour {
    [SerializeField] private float upwardForce;

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
        }

    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            LowGravity(col.gameObject);
        }
    }

    private void LowGravity(GameObject affectedObject)
    {
        affectedObject.GetComponent<Rigidbody>().AddForce((Vector3.up * upwardForce), ForceMode.Force);
    }
}

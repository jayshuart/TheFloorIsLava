using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thrownLowGravityAOE : MonoBehaviour {
    [SerializeField] private float upwardForce;
    [SerializeField] private float scaleRate;
    private Vector3 fullSize;

	// Use this for initialization
	void Start () {
        //save inital scale and then shrink
        fullSize = this.gameObject.transform.localScale;
        this.gameObject.transform.localScale = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
        //scale up low grav aoe
        this.gameObject.transform.localScale = Vector3.Slerp(Vector3.zero, fullSize, scaleRate); 
        scaleRate += scaleRate * Time.deltaTime; 
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

    private void SLERPAoE(float rate)
    {
    }
}

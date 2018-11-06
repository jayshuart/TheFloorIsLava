using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thrownLowGravityAOE : MonoBehaviour {
    [SerializeField] private float upwardForce;
    [SerializeField] private float scaleTime;
    [SerializeField] private float scaleSpeed;
    private Vector3 fullSize;
    [SerializeField] float decayTime;
    private bool decaying;

	// Use this for initialization
	void Start () {
        //save inital scale and then shrink
        fullSize = this.gameObject.transform.localScale;
        this.gameObject.transform.localScale = Vector3.zero;
        decaying = false;
        StartCoroutine(Scale(Vector3.zero, fullSize, scaleTime)); //start recursive grow -> shrink -> destory
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
        //apply low grvaity effect (constant upward force) on players
        if (col.gameObject.CompareTag("Player"))
        {
            LowGravity(col.gameObject);
        }
    }

    private void LowGravity(GameObject affectedObject)
    {
        affectedObject.GetComponent<Rigidbody>().AddForce((Vector3.up * upwardForce), ForceMode.Force);
    }

    /// <summary>
    /// recursive scaling coroutine. Sacles form star size to end over the duration period. Waits. Then scales down and destorys this game objects.
    /// </summary>
    /// <param name="startSize">Start size.</param>
    /// <param name="endSize">End size.</param>
    /// <param name="duration">Duration.</param>
    IEnumerator Scale(Vector3 startSize, Vector3 endSize, float duration)
    {
        //scale
        float i = 0.0f;
        float scaleRate = (1.0f / duration) * scaleSpeed;

        while (i < 1.0f)
        {
            i += Time.deltaTime * scaleRate;
            this.gameObject.transform.localScale = Vector3.Slerp(startSize, endSize, i); 
            yield return null;
        }

        //check if its decaying
        if (decaying)
        {
            //yes destroy this game object
            Destroy(this.gameObject);
        }
        else //not decaying
        {
            decaying = true; //start decay
            yield return new WaitForSeconds(decayTime); //wait a little to hold full size
            StartCoroutine(Scale(fullSize, Vector3.zero, decayTime)); //shrinks - end of this will destory this gameobject
        }
    }
}

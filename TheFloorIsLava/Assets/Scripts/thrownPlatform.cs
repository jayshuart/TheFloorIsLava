using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thrownPlatform : MonoBehaviour {

    [SerializeField] private Vector3 originalPos;
    [SerializeField] private float dropDistance;
    [SerializeField] private float dropRate;
    private float distCovered;
    private bool solid;

    // Use this for initialization
    void Start () {
        solid = false;
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
            originalPos = this.transform.position;
            solid = true;
        }
    }

    void OnCollisionStay(Collision col)
    {
        //sink down if a player is on it
        if (col.gameObject.CompareTag("Player") && solid)
        {
            Sink(Vector3.down); //down
        }
    }

    // <summary>
    /// Sink the specified direction
    /// </summary>
    /// <param name="direction">Direction.</param>
    private void Sink(Vector3 direction)
    {
        distCovered += dropRate * Time.deltaTime;
        this.transform.position = Vector3.Lerp(originalPos, ((direction * dropDistance) + originalPos), distCovered); //lerp change in pos for smooth movement
    }

   
}

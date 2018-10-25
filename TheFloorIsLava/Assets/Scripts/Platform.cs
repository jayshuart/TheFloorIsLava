using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Platform : NetworkBehaviour {

    [SerializeField] private Vector3 originalPos;
    [SerializeField] private float dropDistance;
    [SerializeField] private float dropRate;
    private float distCovered;

	// Use this for initialization
	void Start () {
        originalPos = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        //check if it needs to be moved back to original pos
        if (Mathf.Abs(this.gameObject.transform.position.magnitude ) < Mathf.Abs(originalPos.magnitude)) 
        {
            Sink(Vector3.up); //up
        }
	}

    /// <summary>
    /// Sink the specified direction
    /// </summary>
    /// <param name="direction">Direction.</param>
    private void Sink(Vector3 direction)
    {
        distCovered += dropRate * Time.deltaTime;
        this.transform.position = Vector3.Lerp(originalPos, ((direction * dropDistance) + originalPos), distCovered); //lerp change in pos for smooth movement
    }

    void OnCollisionStay(Collision col)
    {
        //sink down if a player is on it
        if (col.gameObject.CompareTag("Player"))
        {
            Sink(Vector3.down); //down
        }
    }

}

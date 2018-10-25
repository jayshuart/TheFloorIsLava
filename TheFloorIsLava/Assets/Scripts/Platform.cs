using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Platform : NetworkBehaviour {

    [SerializeField] private Vector3 originalPos;
    [SerializeField] private float dropDistance;
    [SerializeField] private float dropRate;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

    private void Sink()
    {
        this.transform.position = Vector3.Lerp(originalPos, ((this.transform.forward * -1) * dropDistance), dropRate);
        dropRate += dropRate * Time.deltaTime;
    }

    void OnCollisionStay(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Sink();
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCam : MonoBehaviour {
    public GameObject ply;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //constantly set position to that of our players - once its been set
        if (ply != null)
        {
            this.gameObject.transform.position = ply.transform.position;
            this.gameObject.transform.rotation = ply.transform.rotation;
            this.gameObject.transform.localScale = ply.transform.localScale;
        }

	}
}

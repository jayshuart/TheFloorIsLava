﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class StateManager : NetworkBehaviour {

	// PUBLIC
	public GameObject playerChar;		// the character
	public GameObject lavaObj;			// lava object. May need to be an array in the future

	// PRIVATE
	private Rigidbody charRig;			// character's rigidbody
	private Rigidbody lavaRig;			// laval's rigidbody
	private GameObject finishLine;		// win state

	// Use this for initialization
	void Start () {
		//this.playerChar = playerChar;
		Cursor.visible = false;

		charRig = playerChar.GetComponent<Rigidbody> ();
		lavaRig = lavaObj.GetComponent <Rigidbody> ();

		finishLine = GameObject.FindGameObjectWithTag ("Finisher"); // finish line instantiation
	}

	void ReachFinishLine(GameObject other) {
		//Physics.Raycast ();
		if (other.tag == "Finisher") {
			Debug.Log ("Finish");
		}
	}
	// Update is called once per frame
	void Update () {
		
	}
}

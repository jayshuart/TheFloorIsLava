using System.Collections;
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

	// Use this for initialization
	void Start () {
		//this.playerChar = playerChar;
		Cursor.visible = false;

		charRig = playerChar.GetComponent<Rigidbody> ();
		lavaRig = lavaObj.GetComponent <Rigidbody> ();
	}

	void OnCollisionEnter(Collision col) {

	}

	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class StateManager : NetworkBehaviour {

	public GameObject playerChar;		// the character
	public GameObject lavaObj;			// lava object. May need to be an array in the future
	// Use this for initialization
	void Start () {
		this.playerChar = playerChar;
	}

	// Update is called once per frame
	void Update () {
		
	}
}

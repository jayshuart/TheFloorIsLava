using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CameraManager : NetworkBehaviour {

	// Use this for initialization
	void Start () {
		if (isLocalPlayer)
			return;

		this.gameObject.GetComponent<Camera>().enabled = false;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CameraManager : NetworkBehaviour {

	// Use this for initialization
	void Start () {
		if (isLocalPlayer)
			return;

		Camera myCamera = this.gameObject.GetComponentInChildren<Camera> ();

		this.gameObject.GetComponentInChildren<Camera>().gameObject.SetActive(false);
	}
}

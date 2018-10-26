using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class StateManager : NetworkBehaviour {

	// PUBLIC
	public GameObject playerChar;		// the character
	public GameObject lavaObj;			// lava object. May need to be an array in the future
	public Text timeScore;				// time taken to complete level for each player

	// PRIVATE
	private Rigidbody charRig;			// character's rigidbody
	private Rigidbody lavaRig;			// laval's rigidbody
	private GameObject finishLine;		// win state
	private float elapsedTime;			// total time since start of game
	//public Text totalScore;				// final time

	// Use this for initialization
	void Start () {
		Cursor.visible = false;

		//playerChar = GameObject.FindGameObjectWithTag ("Player");
		finishLine = GameObject.FindGameObjectWithTag ("Finisher"); // finish line instantiation

		elapsedTime = 0;

		timeScore.color = Color.white;

		//totalScore.transform.SetParent (GameObject.FindGameObjectWithTag (""));
	}

	void Awake() {
		if (playerChar != null) {
			charRig = playerChar.GetComponent<Rigidbody> ();
			lavaRig = lavaObj.GetComponent <Rigidbody> ();
		}
	}

	void ReachFinishLine(GameObject other) {
		Collider tempCollider = other.GetComponent<Collider> ();
		Collider tempCollider2 = playerChar.GetComponent<Collider> ();

		if (other.tag == "Finisher" && tempCollider2.bounds.Intersects (tempCollider.bounds)) {
			// display the time taken to reach finish
			///Debug.Log (elapsedTime.ToString ("0.00"));
			//Debug.Log ("Trigger!");
		} else {
			Debug.Log ("Unknown error/exception");
		}
		//Debug.Log (elapsedTime.ToString("0.000"));
	}


	/*void OnTriggerEnter (Collider col) {
		if (col.name == "Finisher") {
			Debug.Log ("Triggered");
			Debug.Log (elapsedTime.ToString("0.000"));
		}
	}
	*/

	// Update is called once per frame
	void Update () {
		elapsedTime += Time.deltaTime;
		ReachFinishLine (finishLine);

		if (playerChar == null) 
			playerChar = GameObject.FindGameObjectWithTag ("Player");

		timeScore.text = "Time: " + elapsedTime.ToString("0.00");

	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StateManager : NetworkBehaviour {

	// PUBLIC
	public GameObject playerChar;					// the character
	public GameObject lavaObj;						// lava object. May need to be an array in the future
	public Text timeScore;							// time taken to complete level for each player

	// PRIVATE
	private Rigidbody charRig;						// character's rigidbody
	private Rigidbody lavaRig;						// laval's rigidbody
	private GameObject finishLine;					// win state
	[SerializeField] private GameObject[] nwPlayers;// the players in the level
	private float elapsedTime;						// total time since start of game
	[SerializeField] private NetworkManager cm;		// network manager
	//public Text totalScore;						// final time

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
			playerChar.GetComponent<PlayerBehavior>().reachFinish = true;
			playerChar.GetComponent<PlayerBehavior> ().TeleportBackToLobby ();
		} else {
			//Debug.Log ("Unknown error/exception");
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

	/// <summary>
	/// All this is is a small snippet to help transition from scene to scene
	/// </summary>
	/// <param name="level">Level.</param>
	public void GoToNextLevel(string level) {
		cm.ServerChangeScene(level);
	}

	// Update is called once per frame
	void Update () {
		elapsedTime += Time.deltaTime;
		ReachFinishLine (finishLine);

		if (playerChar == null) 
			playerChar = GameObject.FindGameObjectWithTag ("Player");

		timeScore.text = "Time: " + elapsedTime.ToString("0.00");

		// populate the player array with all players in the scene
		nwPlayers = GameObject.FindGameObjectsWithTag("Player");

	}
}

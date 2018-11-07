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
	public GameObject lobbyResPoint;				// point to throw character back into lobby
	public Text timeScore;							// time taken to complete level for each player
	public Text totalScore;							// final time

	// PRIVATE
	private Rigidbody charRig;						// character's rigidbody
	private GameObject finishLine;					// win state
	[SerializeField] private GameObject[] nwPlayers;// the players in the level
	private float elapsedTime;						// total time since start of game
    [SerializeField] private CustomNetwork cm;		// network manager
    [SerializeField] string nextScene;

	// Use this for initialization
	void Start () {
		Cursor.visible = false;

		finishLine = GameObject.FindGameObjectWithTag ("Finisher"); // finish line instantiation

		elapsedTime = 0;
	}

	void Awake() {
		if (playerChar != null) {
			charRig = playerChar.GetComponent<Rigidbody> ();
		}
	}

	void ReachFinishLine(GameObject other) {
		if (playerChar == null) {
			return;
		}
		Collider tempCollider = other.GetComponent<Collider> ();
		//Collider tempCollider2 = playerChar.GetComponent<Collider> ();

		if (other.tag == "Finisher" && playerChar.GetComponent<Collider>().bounds.Intersects (tempCollider.bounds)) {
			// display the time taken to reach finish
			totalScore.text = elapsedTime.ToString("0.00");
			playerChar.GetComponent<PlayerBehavior>().reachFinish = true;
			playerChar.GetComponent<PlayerBehavior> ().TeleportBackToLobby ();
            GoToNextLevel();
		} else {
			//Debug.Log ("Unknown error/exception");
		}
		//Debug.Log (elapsedTime.ToString("0.000"));
	}

	/// <summary>
	/// All this is is a small snippet to help transition from scene to scene
	/// </summary>
	public void GoToNextLevel() {
        
		cm.ServerChangeScene(nextScene);
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

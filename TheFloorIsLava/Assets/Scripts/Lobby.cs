using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Lobby : NetworkBehaviour {

    [SerializeField] private CustomNetwork networkManager;
    public bool nextLevel; //check for if we are chaning level or starting level
    [SerializeField] string nextScene;
    public bool respawnFlag;

	// Use this for initialization
	void Awake () {
        networkManager = GameObject.Find("NetworkManager_Custom").GetComponent<CustomNetwork>();
        nextLevel = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
        
    void OnTriggerEnter(Collider col)
    {
        //check if this is the host
        if(col.gameObject.CompareTag("Player") && col.gameObject == networkManager.GetPlayerAt(0))
        {
            if (nextLevel)
            {
                networkManager.ServerChangeScene(nextScene);
                return; //leave method
            }

            //move all players to the actual level
            foreach (GameObject ply in networkManager.Players)
            {
                //save this player bhevaior script
                PlayerBehavior plyScript = ply.GetComponent<PlayerBehavior>();

                //reset players spawnpoint
                plyScript.spawnPoint = GameObject.FindGameObjectWithTag("Respawn").transform; //normal spawn point

                plyScript.respawnFlag = true;
                //force respawn
                //plyScript.PlayerForcedRespawn(true); //force respawn with btn overrride set to true
            }




            //make it so next pass wil goto next level instead of restarting this one
            nextLevel = true;
        }
    }
}

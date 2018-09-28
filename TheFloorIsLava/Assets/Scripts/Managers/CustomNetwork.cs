using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CustomNetwork : NetworkManager {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void OnServerAddPlayer (NetworkConnection conn, short playerControllerId)
    {

        GameObject player;

        player = (GameObject)Object.Instantiate(this.playerPrefab, Vector3.zero, Quaternion.identity);
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);

    }



    public override void OnClientSceneChanged(NetworkConnection conn)
    {
        ClientScene.AddPlayer(conn, 0);
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        //base.OnClientConnect(conn);
    }
}

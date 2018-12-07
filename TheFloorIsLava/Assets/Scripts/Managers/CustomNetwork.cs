using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CustomNetwork : NetworkManager {
    [SerializeField] private List<GameObject> players;

    #region Properties
    //properties
    public List<GameObject> Players
    {
        get{ return players; }
    }

    public GameObject GetPlayerAt(int index)
    {
        if (index > (players.Count - 1) || index < 0)
        {
            return null; 
        }
        else
        {
            return players[index];
        }

    }
    #endregion

	// Use this for initialization
	void Start () {
        players = new List<GameObject>();
		
	}
	
	// Update is called once per frame
	void Update () {
     
	}

    /// <summary>
    /// Starts the game with a server and a client (the host) - with access so it cna be hooked up to a button
    /// </summary>
    public void StartGame()
    {
        //runs StartHost in network manager
        StartHost();
    }

    /// <summary>
    /// Joins an already hosted game with a client - with access so it can be hooked up to a button
    /// </summary>
    public void JoinGame()
    {
        //runs startclient() in networkmanager
        StartClient();
    }

    public void EndGame()
    {
        players.Clear();
        //StopServer();
        StopHost();
    }


    public override void OnServerAddPlayer (NetworkConnection conn, short playerControllerId)
    {

        //instantiate a player prefab (defined in the inspector - declared here as this.playerPrefab)
        GameObject player = (GameObject)Object.Instantiate(this.playerPrefab, this.gameObject.transform.position, Quaternion.identity);

        //add to list of players for easy access
        players.Add(player);

        //connect this player tot he server
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);



    }

    public override void OnServerRemovePlayer (NetworkConnection conn, UnityEngine.Networking.PlayerController player)
    {
        players.Remove(player.gameObject); //remove this player from list
        base.OnServerRemovePlayer(conn, player);
    }



    public override void OnClientSceneChanged(NetworkConnection conn)
    {
        //add player tot he client via networkign info (0 because its the client id)
        players.Clear();
        ClientScene.AddPlayer(conn, 0);
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        //base.OnClientConnect(conn); //commented out so this cusotm setup doesnt try to create a player that already exists
    }
        
}

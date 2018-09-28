using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    [SerializeField] private CustomNetwork networkManager;

    [SerializeField] private GameObject levelsParent;
    [SerializeField] private List<GameObject> levelsList = new List<GameObject>();
    [SerializeField] private int currentLevel;

	// Use this for initialization
	void Start () {
        currentLevel = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    /// <summary>
    /// Builds list of levels from the children of the levels parent game object
    /// </summary>
    public void BuildLevelSelect()
    {
        //cycle through the children and add them tot he level list
        for (int i = 0; i < levelsParent.transform.childCount; i++)
        {
            //actually adding them
            levelsList.Add(levelsParent.transform.GetChild(i).gameObject);
        }
    }

    /// <summary>
    /// Changes the selected level by the defined amount (preferably +/- 1)
    /// </summary>
    /// <param name="changeAmount">Change amount.</param>
    public void ChangeLevelSelect(int changeAmount)
    {
        //turn off current level
        levelsList[currentLevel].SetActive(false);

        //turn on next one
        currentLevel = (currentLevel + changeAmount) % (levelsList.Count - 1); //wrapping index so we dont try to acess and index that doesnt exist
        levelsList[currentLevel].SetActive(true); //turn this new current level on
    }

    /// <summary>
    /// changes the active scene on the server if it exists in the build
    /// </summary>
    /// <param name="level">Level.</param>
    public void GotoScene(string level)
    {
        //uses ServerChangeScene so that all players (host and clients) 
        networkManager.ServerChangeScene(level);
    }

    /// <summary>
    /// leave the game
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class doubleJump_shadowOverlay : MonoBehaviour {

    //fields
    private float maxCoolShadow;
    [SerializeField] private float amountLeft;
    [SerializeField] private GameObject localPlayer;
    private Ability_DoubleJump jumpScript;

    //properties
    public GameObject LocalPlayer
    {
        set { localPlayer = value; }
    }

    public Ability_DoubleJump JumpScript
    {
        set { jumpScript = value; }
    }


	// Use this for initialization
	void Start () {
        //default values will be overun when local ply joins
        maxCoolShadow = 1.0f;
        amountLeft = 0.0f;
		
	}
	
	// Update is called once per frame
	void Update () {
        //check to make sure we have a palyer to set values from
        if (localPlayer != null)
        {
            //condition for when cooldown is maxed (not cooling thus 0.0 seconds)
            if (jumpScript.TimeWaited == 0.0f)
            {
                amountLeft = 0.0f;
            }
            else //set amount left normally
            {
                //get new amount of shadow from local ply
                amountLeft = jumpScript.CooldownTime - jumpScript.TimeWaited; //total time - time waited = amount left
            }


            //set fill amount
            this.gameObject.GetComponent<Image>().fillAmount = (amountLeft / jumpScript.CooldownTime); //make a percent out of amount left
        }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shadowOverlay : MonoBehaviour {

    //fields
    private float maxCoolShadow;
    [SerializeField] private float amountLeft;
    [SerializeField] private GameObject localPlayer;
    private float cooldownTime;
    private float timeWaited;

    //properties
    public GameObject LocalPlayer
    {
        set { localPlayer = value; }
    }

    public float CooldownTime
    {
        set { cooldownTime = value; }
    }

    public float TimeWaited
    {
        set { timeWaited = value; }
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
            if (timeWaited == 0.0f)
            {
                amountLeft = 0.0f;
            }
            else //set amount left normally
            {
                //get new amount of shadow from local ply
                amountLeft = cooldownTime - timeWaited; //total time - time waited = amount left
            }


            //set fill amount
            this.gameObject.GetComponent<Image>().fillAmount = (amountLeft / cooldownTime); //make a percent out of amount left
        }
	}
}

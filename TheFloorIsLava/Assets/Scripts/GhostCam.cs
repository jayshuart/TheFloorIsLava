using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCam : MonoBehaviour {
    public GameObject ply;
    private float yaw;
    private bool freecam;

	// Use this for initialization
	void Start () {
        yaw = 0;
		
	}
	
	// Update is called once per frame
	void Update () {
        //constantly set position to that of our players - once its been set
        if (ply != null)
        {
            this.gameObject.transform.position = ply.transform.position;
            //this.gameObject.transform.rotation = ply.transform.rotation;

            this.gameObject.transform.localScale = ply.transform.localScale;

            //check if player is moving forward
            RotateCam("Mouse X");

            if (!Input.GetKey(KeyCode.LeftShift))
            {
                //realign cam
                ReAlignCam();
            }

        }

	}

    //update cam rot
    private void RotateCam(string inputAxis)
    {
        yaw += (6.5f * Input.GetAxis (inputAxis));
        yaw = yaw % 360;

        ply.transform.eulerAngles = new Vector3(0.0f, yaw, 0.0f);    // Euler Angles to prevent gimbal locking (as with previous issue)

        //set plys forward to the new one
        ply.GetComponent<PlayerBehavior>().yaw = this.yaw;
    }

    private void ReAlignCam()
    {
        Quaternion toRotation = Quaternion.LookRotation(transform.forward, ply.transform.rotation.eulerAngles);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, Time.deltaTime);

        this.gameObject.transform.rotation = ply.transform.rotation;

        yaw =  ply.GetComponent<PlayerBehavior>().yaw;
    }
        
}

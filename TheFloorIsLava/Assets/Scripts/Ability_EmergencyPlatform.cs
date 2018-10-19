using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Ability_EmergencyPlatform : NetworkBehaviour {

    [SerializeField] private GameObject throwablePrefab;
    [SerializeField] private float throwForce;
    [SerializeField] private Transform throwStartTransform;
    private Rigidbody throwRB;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isLocalPlayer)
        {
            //left click
            if (Input.GetMouseButtonUp(0))
            {
                //actually throw our emergency platform
                throwPlatform();

                //retunr time to original set
                Time.timeScale = 1.0f;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;
            }
            else if (Input.GetMouseButtonDown(0))
            {
                //slow time
                Time.timeScale = 0.2f;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;
            }
            else if (Input.GetMouseButton(0))
            {
                //show arc
                //PlotTrajectory((this.transform.position + this.transform.forward), (this.transform.forward * throwForce), .002f, 50f);
            }

            //show arc
            PlotTrajectory(throwStartTransform.position, throwRB.velocity, .2f, 5f);

        }
	}

    /// <summary>
    /// Throws the platform.
    /// </summary>
    private void throwPlatform()
    {
        //create platform
        GameObject throwable = GameObject.Instantiate(throwablePrefab, throwStartTransform.position,  this.transform.localRotation);

        //apply force to it
        throwRB = throwable.GetComponent<Rigidbody>();
        Vector3 throwVector = this.transform.forward * throwForce;
        throwRB.AddForce(throwVector, ForceMode.Impulse);
    }


    public Vector3 PlotTrajectoryAtTime (Vector3 start, Vector3 startVelocity, float time) {
        return start + startVelocity*time + Physics.gravity*time*time*0.5f;
    }

    public void PlotTrajectory (Vector3 start, Vector3 startVelocity, float timestep, float maxTime) {
        Vector3 prev = start;
        for (float i = 1; i < maxTime; i = i + timestep)
        {
            Vector3 point = PlotTrajectoryAtTime(start, startVelocity, i);
            Debug.DrawLine(prev, point, Color.cyan);

            prev = point;
        }
    }
}

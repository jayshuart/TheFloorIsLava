using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Ability_EmergencyPlatform : NetworkBehaviour {

    [SerializeField] private GameObject throwablePrefab;
    [SerializeField] private float throwForce;
    [SerializeField] private float forceScaleAmount;
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
                ThrowPlatform();

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
                //scale force
                AdjustForce();
                
                //show arc
                Vector3 throwVelocity = (this.transform.forward * throwForce) * throwRB.mass; //v = m * f
                PlotTrajectory(throwStartTransform.position, throwVelocity, .05f, 2f);;
            }
        }
	}

    /// <summary>
    /// Throws the platform.
    /// </summary>
    private void ThrowPlatform()
    {
        //create platform
        GameObject throwable = GameObject.Instantiate(throwablePrefab, throwStartTransform.position,  this.transform.localRotation);

        //ignore collisions with this throwable and the player
        Physics.IgnoreCollision(this.gameObject.GetComponent<Collider>(), throwable.GetComponent<Collider>());

        //apply force to it
        throwRB = throwable.GetComponent<Rigidbody>();
        Vector3 throwVector = this.transform.forward * throwForce;
        throwRB.AddForce(throwVector, ForceMode.Impulse);
    }

    /// <summary>
    /// Adjusts the force of the htrow via scrolling
    /// </summary>
    private void AdjustForce()
    {
        //get input
        if (Input.mouseScrollDelta.y > 0) //scroll forward (positive)
        {
            //up force
            throwForce += forceScaleAmount;
        }
        else if (Input.mouseScrollDelta.y < 0) //scroll backward (negative)
        {
            //lessen throw force
            throwForce -= forceScaleAmount;
        }
    }


    public Vector3 PlotTrajectoryAtTime (Vector3 start, Vector3 startVelocity, float time) {
        return start + startVelocity*time + Physics.gravity*time*time*0.5f;
    }

    public void PlotTrajectory (Vector3 start, Vector3 startVelocity, float timestep, float maxTime) {
        // get line renderer
        LineRenderer line = throwStartTransform.gameObject.GetComponent<LineRenderer>();

        //make line rednerer have enough space for all the points
        float steps = (maxTime / timestep);
        line.positionCount = (int) steps;

        //set intial point
        int iterator = 0;
        line.SetPosition(iterator, start);

        //loop through time calculting trajectory
        for (float i = timestep; i < maxTime; i = i + timestep)
        {
            //update iterator for line renderer
            iterator++;

            // plot point at thips instance in time
            Vector3 point = PlotTrajectoryAtTime(start, startVelocity, i);

            //draw line
            line.SetPosition(iterator, point);
        }
    }
}

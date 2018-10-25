using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public  class ThrowParent : NetworkBehaviour {

    [SerializeField] protected GameObject throwablePrefab; //prefab of object being thrown

    //force properties
    [SerializeField] protected float throwForce;
    [SerializeField] protected float forceScaleAmount;

    //special throw info
    [SerializeField] protected Transform throwStartTransform; //start point of the throw
    protected Rigidbody throwRB; //rigid body of the thrown object
    protected bool canThrow;

    //ui properties
    protected shadowOverlay uiOverlay; //ui element
    [SerializeField] protected float cooldownTime; //time needed to wait before can throw again
    [SerializeField] protected float timeWaited; //amount of time waited

    //vignette
    protected float tStep = 0;
    [SerializeField] protected float maxIntensity;
    [SerializeField] protected Shader screenEffect;

    // Use this for initialization
    protected void Start () {
        canThrow = true;
        Camera.main.GetComponent<CameraEffect>().SetShader(screenEffect);
    }

    //start but only for local player junk
    public override void OnStartLocalPlayer()
    {
        
    }


    // Update is called once per frame
    protected void Update () {
        if (isLocalPlayer)
        {
            //left click
            if (Input.GetMouseButtonUp(0) && canThrow)
            {
                //actually throw our emergency platform
                ThrowObject();

                //retunr time to original set
                Time.timeScale = 1.0f;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;

                //remove vignette
                Camera.main.GetComponent<CameraEffect>().intensity = 0;
                tStep = 0;
            }
            else if (Input.GetMouseButtonDown(0) && canThrow)
            {
                //slow time
                Time.timeScale = 0.2f;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;
            }
            else if (Input.GetMouseButton(0) && canThrow)
            {
                //scale force
                AdjustForce();

                //show arc
                Vector3 throwVelocity = (this.transform.forward * throwForce); //v = m * f
                PlotTrajectory(throwStartTransform.position, throwVelocity, .05f, 1f);;

                //cam effect
                Camera.main.GetComponent<CameraEffect>().intensity = Mathf.Lerp(0, maxIntensity, tStep);
                tStep += 50f * Time.deltaTime;
            }

            //update UI and cooldown timer
            CoolDown(); 
        }
    }

    protected void InitializeUI(string uiName)
    {
        uiOverlay = GameObject.Find(uiName).GetComponentInChildren<shadowOverlay>();
        uiOverlay.LocalPlayer = this.gameObject;
        uiOverlay.CooldownTime = this.cooldownTime;
        uiOverlay.TimeWaited = this.timeWaited;
    }

    /// <summary>
    /// Throws the platform.
    /// </summary>
    protected void ThrowObject()
    {
        //make us unable to throw again
        canThrow = false;
        timeWaited = 0;

        //create platform
        GameObject throwable = GameObject.Instantiate(throwablePrefab, throwStartTransform.position,  this.transform.localRotation);

        //apply force to it
        throwRB = throwable.GetComponent<Rigidbody>();
        Vector3 throwVector = this.transform.forward * throwForce;
        throwRB.AddForce(throwVector, ForceMode.Impulse);
    }

    /// <summary>
    /// Adjusts the force of the htrow via scrolling
    /// </summary>
    protected void AdjustForce()
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

    /// <summary>
    /// Plots the trajectory at time.
    /// </summary>
    /// <returns>The trajectory at time.</returns>
    /// <param name="start">Start position of object</param>
    /// <param name="startVelocity">Start velocity of object</param>
    /// <param name="time">moment in time</param>
    protected Vector3 PlotTrajectoryAtTime (Vector3 start, Vector3 startVelocity, float time) {
        // thanks - https://answers.unity.com/questions/296749/display-arc-for-cannons-ball-trajectory.html
        return start + startVelocity*time + Physics.gravity*time*time*0.5f;
    }

    /// <summary>
    /// draws physical line to plot the trajectory arc of the thrown object
    /// </summary>
    /// <param name="start">Start position</param>
    /// <param name="startVelocity">inital velocity</param>
    /// <param name="timestep">time between each trajectory point</param>
    /// <param name="maxTime">Max time ploted</param>
    protected void PlotTrajectory (Vector3 start, Vector3 startVelocity, float timestep, float maxTime) {
        // get line renderer
        LineRenderer line = throwStartTransform.gameObject.GetComponent<LineRenderer>();

        //make line rednerer have enough space for all the points
        float steps = (maxTime / timestep);
        line.positionCount = (int) (steps);

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

    /// <summary>
    /// reanables throwing after cooldown time has passed - also updates HUD element
    /// </summary>
    protected void CoolDown()
    {
        //update HUD info
        uiOverlay.CooldownTime = this.cooldownTime;
        uiOverlay.TimeWaited = this.timeWaited;

        //check if we need to even do a cooldown (maybe they have all their jumps?)
        if (!canThrow)
        {
            //up time waited
            timeWaited += Time.deltaTime;

            //check if we have waited long enough to use double jump
            if (timeWaited >= cooldownTime)
            {
                //allow to throw again
                canThrow = true;
            }
        }
    }
}

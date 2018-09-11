// RIPPED FROM NETWORKING TUTORIAL NOT FOR ACTUAL MOVEMENT SCRIPT
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour //needs to be a networked behavior not monobehavior
{
    void Update()
    {
        //check we are the player that wants input (our local bud)
        if (!isLocalPlayer)
        {
            //we are not the local player- get out
            return;
        }

        //get axis information
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        //actually move the boi
        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);
    }
}
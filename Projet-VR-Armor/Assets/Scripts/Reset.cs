using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour {


    public Transform cameraRigTransform;
    public Transform headTransform; // The camera rig's head

    // Use this for initialization
    void Start () {
		
	}
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("r"))
        {
            Vector3 difference = cameraRigTransform.position - headTransform.position; // Calculate the difference between the center of the virtual room & the player's head
            difference.y = 0; // Don't change the final position's y position, it should always be equal to that of the hit point

            cameraRigTransform.position = new Vector3(-4.979447f, -30.57413f, -26.854f) + difference; // Change the camera rig position to where the the teleport reticle was. Also add the difference so the new virtual room position is relative to the player position, allowing the player's new position to be exactly where they pointed. (see illustration)
        }

    }
}

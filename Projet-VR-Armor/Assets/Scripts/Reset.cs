using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour {

//Ce script permet la téleportation du casque au coordonnées voulu en appuiant sur la touche "r"
    public Transform cameraRigTransform;
    public Transform headTransform; // The camera rig's head
    public float x;
    public float y;
    public float z;

    // Use this for initialization
    void Start () {
		
	}
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("r"))
        {
            Vector3 difference = cameraRigTransform.position - headTransform.position; // Calculate the difference between the center of the virtual room & the player's head
            difference.y = 0; // Don't change the final position's y position, it should always be equal to that of the hit point

            cameraRigTransform.position = new Vector3(x,y,z) + difference; // Change the camera rig position to where the the teleport reticle was. Also add the difference so the new virtual room position is relative to the player position, allowing the player's new position to be exactly where they pointed. (see illustration)
        }

    }
}

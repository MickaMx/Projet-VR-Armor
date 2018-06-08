using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class AffichageToolTip : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter(Collider other)
    {
        GetComponentInChildren<Canvas>().enabled = true;
    }

    public void OnTriggerExit(Collider other)
    {
        GetComponentInChildren<Canvas>().enabled = false;
    }
}

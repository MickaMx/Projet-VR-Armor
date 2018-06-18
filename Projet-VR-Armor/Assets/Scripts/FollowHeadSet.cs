using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class FollowHeadSet : MonoBehaviour {

    public Camera HeadSet;
    public Camera source;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        source.CopyFrom(HeadSet);
    }
}

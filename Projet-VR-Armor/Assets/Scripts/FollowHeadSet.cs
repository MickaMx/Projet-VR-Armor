using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHeadSet : MonoBehaviour {

    public GameObject HeadSet;
    public GameObject source;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        source.GetComponent<Transform>().position = HeadSet.GetComponent<Transform>().position;
        Vector3 temp = source.GetComponent<Transform>().position;
        temp.y -= 0.8f;
        source.GetComponent<Transform>().rotation = HeadSet.GetComponent<Transform>().rotation;

    }
}

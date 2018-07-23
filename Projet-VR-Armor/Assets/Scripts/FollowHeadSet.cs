using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class FollowHeadSet : MonoBehaviour {

    //Ce script permet qu'une caméra copie exactement les mouvements d'une autre. Il est utile pour associer le casque avec le nuage de points

    public Camera Suiveur;
    public Camera Source;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Suiveur.CopyFrom(Source);
    }
}

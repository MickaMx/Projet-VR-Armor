using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class PlayAnimationG : MonoBehaviour {


    public ViveControllerInputTest LeftController;
    private SteamVR_TrackedObject trackedObj;

    private int flag;
    private bool firstuse;

    bool firstHit;  //Flag pour la succession de la selection des points.
    bool secondHit;

    // Use this for initialization
    void Start () {
        flag = 0;
        firstuse = true;
	}


    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        trackedObj = LeftController.GetComponent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void Update () {


        if (firstHit && secondHit)//Actif quand deux tirs ont été réalisé
        {
            firstHit = false;//Remise a zero
            secondHit = false;
        }
        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            if (!firstHit)//si premier tir
            {
                Debug.Log("play ouverture");
                firstHit = true;
                GetComponent<Animation>().Play("OuvertureCouvercleG");
                return;
            }
            if (firstHit && !secondHit)//si second tir
            {
                Debug.Log("play Fermeture");
                GetComponent<Animation>().Play("FermetureCouvercleG");
                secondHit = true;
                return;
            }
        }


    }
}

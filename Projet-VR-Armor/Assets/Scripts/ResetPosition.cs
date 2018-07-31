using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;



//ce script permet en cas d'appuie sur le boutton selectionner de changer la position du casque suivant les coordonnées x,y,z.
public class ResetPosition : MonoBehaviour
{

    [Header("Input")]
    public ViveControllerInputTest LeftController;
    public ViveControllerInputTest.Boutton Button;

    [Header("Paramètres")]
    public Transform cameraRigTransform;
    public Transform HeadTransform;
    public float x;
    public float y;
    public float z;

    private bool boolButton;



    // Use this for initialization
    void Start()
    {
    }



    // Update is called once per frame
    void Update()
    {
        try
        {
            switch ((int)Button)
            {
                case 0:
                    boolButton = LeftController.ApplicationMenu;
                    break;
                case 1:
                    boolButton = LeftController.Grip;
                    break;
                case 2:
                    boolButton = LeftController.Touchpad;
                    break;
                case 3:
                    boolButton = LeftController.Trigger;
                    break;
            }

            if (LeftController.ApplicationMenu)
            {
                Vector3 offset = cameraRigTransform.position - HeadTransform.position;//calcul de l'offset
                offset.y = 0;
                cameraRigTransform.position = new Vector3(x, y, z) + offset; //on change la position du casque avec un offset pour rester au meme endroit dans le cameraRig
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning(e.Message);
        }
    }
}

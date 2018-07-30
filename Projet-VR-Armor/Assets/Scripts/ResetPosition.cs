using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public enum Buttonreset //sert à selectionner un boution dans l'inspector dans une liste déroulante. Peut etre utiliser dans un autre script (exemple Reset.cs)
{
    ulongSystem, // reserved
    ApplicationMenu,
    Axis0,
    Axis1,
    Axis2,
    Axis3,
    Axis4,
    Grip,
    Touchpad,
    Trigger
}

//ce script permet en cas d'appuie sur le boutton selectionner de changer la position du casque suivant les coordonnées x,y,z.
public class ResetPosition : MonoBehaviour
{

    [Header("Input")]
    public Buttonreset Button = Buttonreset.ApplicationMenu;
    private int ButtonInt;
    private ulong ButtonULong;
    public ViveControllerInputTest LeftController;

    [Header("Paramètres")]
    public Transform cameraRigTransform;
    public Transform HeadTransform;
    public float x;
    public float y;
    public float z;
    private SteamVR_TrackedObject trackedObj;



    // Use this for initialization
    void Start()
    {
        ButtonInt = (int)Button;

        switch (ButtonInt)//Selection du boutton
        {
            case 0:
                ButtonULong = (1ul << (int)EVRButtonId.k_EButton_System);
                break;
            case 1:
                ButtonULong = (1ul << (int)EVRButtonId.k_EButton_ApplicationMenu);
                break;
            case 2:
                ButtonULong = (1ul << (int)EVRButtonId.k_EButton_Axis0);
                break;
            case 3:
                ButtonULong = (1ul << (int)EVRButtonId.k_EButton_Axis1);
                break;
            case 4:
                ButtonULong = (1ul << (int)EVRButtonId.k_EButton_Axis2);
                break;
            case 5:
                ButtonULong = (1ul << (int)EVRButtonId.k_EButton_Axis3);
                break;
            case 6:
                ButtonULong = (1ul << (int)EVRButtonId.k_EButton_Axis4);
                break;
            case 7:
                ButtonULong = (1ul << (int)EVRButtonId.k_EButton_Grip);
                break;
            case 8:
                ButtonULong = (1ul << (int)EVRButtonId.k_EButton_SteamVR_Touchpad);
                break;
            case 9:
                ButtonULong = (1ul << (int)EVRButtonId.k_EButton_SteamVR_Trigger);
                break;

        }
    }


    private SteamVR_Controller.Device Controller
    {
        get
        {
            try
            {
                return SteamVR_Controller.Input((int)trackedObj.index);

            }
            catch (Exception e)
            {
                return null;
            }
        }
    }

    void Awake()
    {
        trackedObj = LeftController.GetComponent<SteamVR_TrackedObject>();
    }


    // Update is called once per frame
    void Update()
    {
        try
        {
            if (Controller.GetPressDown(ButtonULong))
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

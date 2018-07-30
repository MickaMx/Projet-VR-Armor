using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using VRTK;

//ce script permet en cas d'appuie sur le boutton selectionner de remettre tous les objects déplacables a leurs positions initiales. 

public class Reset : MonoBehaviour
{
    [Header("Input")]
    public Buttonreset Button = Buttonreset.ApplicationMenu;
    public ViveControllerInputTest LeftController;

    private int ButtonInt;
    private ulong ButtonULong;
    private VRTK_InteractableObject[] allObjectsInteractable;
    private AffichageToolTip[] allObjectsTooltip;
    private Vector3[] positionInitial;
    private Quaternion[] positionInitialAnim;
    private Animation[] allObjectsAnimation;
    private SteamVR_TrackedObject trackedObj;


    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        trackedObj = LeftController.GetComponent<SteamVR_TrackedObject>();
    }

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
        //Création des tableaux d'objects
        allObjectsInteractable = FindObjectsOfType<VRTK_InteractableObject>() as VRTK_InteractableObject[];
        allObjectsTooltip = FindObjectsOfType<AffichageToolTip>() as AffichageToolTip[];
        allObjectsAnimation = FindObjectsOfType<Animation>() as Animation[];
        positionInitial = new Vector3[allObjectsInteractable.Length];
        positionInitialAnim = new Quaternion[allObjectsAnimation.Length];


        for (int i = 0; i < allObjectsInteractable.Length; i++)//Récupération des coordonnées initiales des objects déplacables
        {
            positionInitial[i] = allObjectsInteractable[i].gameObject.transform.position;
        }

        for (int i = 0; i < allObjectsAnimation.Length; i++)//Récupération des coordonnées initiales des objects animé
        {
            positionInitialAnim[i] = allObjectsAnimation[i].gameObject.transform.rotation;
        }


    }
    // Update is called once per frame
    void Update()
    {

        if (Controller.GetPressDown(ButtonULong))//si appui
        {

            for (int i = 0; i < allObjectsInteractable.Length; i++)
            {
                allObjectsInteractable[i].GetComponent<Rigidbody>().isKinematic = true;//désactivation de la physique pour éviter qu'un object en mouvement reste en mouvement apres son reset
                allObjectsInteractable[i].gameObject.transform.position = positionInitial[i];//Reset
            }

            for (int i = 0; i < allObjectsAnimation.Length; i++)
            {
                allObjectsAnimation[i].gameObject.transform.rotation = positionInitialAnim[i];
            }

            for (int i = 0; i < allObjectsTooltip.Length; i++)
            {
                allObjectsTooltip[i].GetComponent<Renderer>().material = allObjectsTooltip[i].GetOriginMaterial();
                allObjectsTooltip[i].GetComponent<Renderer>().material.shader = Shader.Find("Standard");
            }

        }
        if (Controller.GetPressUp(ButtonULong))//si relache
        {
            for (int i = 0; i < allObjectsInteractable.Length; i++)
            {
                allObjectsInteractable[i].GetComponent<Rigidbody>().isKinematic = false;//re-activation de la physique
            }
        }
    }

}

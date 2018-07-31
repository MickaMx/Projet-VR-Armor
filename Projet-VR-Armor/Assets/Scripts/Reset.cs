using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using VRTK;

//ce script permet en cas d'appuie sur le boutton selectionner de remettre tous les objects déplacables a leurs positions initiales. 

public class Reset : MonoBehaviour
{
    [Header("Input")]
    public ViveControllerInputTest LeftController;
    public ViveControllerInputTest.Boutton Button;

    private VRTK_InteractableObject[] allObjectsInteractable;
    private AffichageToolTip[] allObjectsTooltip;
    private Vector3[] positionInitial;
    private Quaternion[] positionInitialAnim;
    private Animation[] allObjectsAnimation;
    private bool flag = true;
    private bool boolButton;




    // Use this for initialization
    void Start()
    {        
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

            if (LeftController.Trigger && flag)//si appui
            {
                flag = false;
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
            if (!LeftController.Trigger && !flag)//si relache
            {
                flag = true;
                for (int i = 0; i < allObjectsInteractable.Length; i++)
                {
                    allObjectsInteractable[i].GetComponent<Rigidbody>().isKinematic = false;//re-activation de la physique
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning(e.Message);
        }
    }

}

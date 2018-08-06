using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;



//ce script permet en cas d'appuie sur le boutton selectionner de changer la position du casque suivant les coordonnées x,y,z.
public class ResetPosition : MasterScript
{
    [Tooltip("Manette à suivre pour les appuis sur bouton")]
    public ViveControllerInputTest LeftController;

    [Header("Paramètres")]
    [Tooltip("Transform de la zone de l'utilisateur")]
    public Transform cameraRigTransform;
    [Tooltip("Transform de la position de l'utilisateur dans cette zone ")]
    public Transform HeadTransform;
    [Tooltip("Coordonnées du reset")]
    public float x;
    [Tooltip("Coordonnées du reset")]
    public float y;
    [Tooltip("Coordonnées du reset")]
    public float z;

    // Use this for initialization
    void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {
        try
        {
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

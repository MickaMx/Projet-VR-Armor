using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    //Script servant à lancer une animation lors d'un appuie sur un bouton.
    [Header("Joue les animations en alternance")]
    [Header(" lors d'un appui sur le bouton.")]

    public GameObject objectAAnimer;
    public string Animation1;
    public string Animation2;

    private bool firstHit;  //Flag pour la succession de la selection des points.
    private bool secondHit;
    private int i;
    private Animation anim;
    // Use this for initialization
    void Start()
    {
        i = 0;
        anim = objectAAnimer.GetComponent<Animation>();
    }
    

    void OnTriggerEnter(Collider other)
    {
        //if (i == 0)
       // {
            if (firstHit && secondHit)//Actif quand deux tirs ont été réalisé
            {
                firstHit = false;//Remise a zero
                secondHit = false;
            }
            if (!firstHit)//si premier tir
            {
                // Debug.Log("play ouverture");
                firstHit = true;
                i++;
                anim.Play(Animation1);
                return;
            }
            if (firstHit && !secondHit)//si second tir
            {
                // Debug.Log("play Fermeture");
                secondHit = true;
                i++;
                anim.Play(Animation2);
                return;
            }
        //}
    }

    /*void OnTriggerExit(Collider other)
    {
        if (i == 1)//évite la répétition des évènements OnTriggerExit;
        {
            i = 0;
        }
    }*/
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Script servant a calculer la distance entre deux points, a utiliser avec le script RaycastAffichage

public class CalculDist : MonoBehaviour
{
    public GameObject laserPrefab;
    public GameObject HitPointSphere; //Matérialisation du point d'impact
    public int echelle;
    public float Taille_Sphère;

    private GameObject laser; //Laser à instancier
    private Transform laserTransform;//Cordonnées du laser
    private float dist;



    bool firstHit;  //Flag pour la succession de la selection des points.
    bool secondHit;
    Vector3 hitpoint1;  //Coordonnées des points d'impacts
    Vector3 hitpoint2;
    GameObject sphere2; //Matérialisation du point d'impact
    GameObject sphere1;
    // Use this for initialization
    void Start()
    {
        laser = Instantiate(laserPrefab);
        laserTransform = laser.transform;
        firstHit = false;
        secondHit = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (firstHit && secondHit)//Actif quand deux tirs ont été réalisé
        {
            
            firstHit = false;//Remise a zero
            secondHit = false;
            hitpoint1 = new Vector3();
            hitpoint2 = new Vector3();
        }
        if (Input.GetMouseButtonUp(1))//Si clic gauche
        {
            if (!firstHit)//si premier tir
            {
                laser.SetActive(false);
                DestroyObject(sphere1);//Destruction des anciennes sphères
                DestroyObject(sphere2);
                //Debug.Log("RAZ");

                //Debug.Log("First hit");
                hitpoint1 = GetComponent<RaycastAffichage>().hitPointToSend; // récupération des coordonnées du premier tir
                sphere1 = GameObject.Instantiate(HitPointSphere, hitpoint1,new Quaternion()); // création de la spère au point d'impact
                sphere1.transform.localScale = new Vector3(Taille_Sphère, Taille_Sphère, Taille_Sphère);//scale de la sphère  
                firstHit = true;
                return;
            }
            if (firstHit && !secondHit)//si second tir
            {
                //Debug.Log("Second hit");
                hitpoint2 = GetComponent<RaycastAffichage>().hitPointToSend;// récupération des coordonnées du premier tir
                sphere2 = GameObject.Instantiate(HitPointSphere, hitpoint2, new Quaternion());// création de la spère au point d'impact
                sphere2.transform.localScale = new Vector3(Taille_Sphère, Taille_Sphère, Taille_Sphère);//scale de la sphère                             
                secondHit = true;
                dist = Vector3.Distance(hitpoint1, hitpoint2);// calcul de la distance entre les deux spères
                ShowLaser();
                
                dist = echelle * dist;//mise à l'échelle
                laser.GetComponent<AffichageFlottant>().calloutLabel = dist.ToString();               
                //Debug.Log(dist);
                return;
            }
        } 
    }

    private void ShowLaser()
    {
        laser.SetActive(true);//Affichage du laser
        laserTransform.position = Vector3.Lerp(hitpoint1, hitpoint2, .5f);//Coordonnées du point d'origine
        laserTransform.LookAt(hitpoint2);
        laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y, dist);
    }
}
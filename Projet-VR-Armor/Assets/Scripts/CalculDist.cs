using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

//Script servant a calculer la distance entre deux points. Les distances X et Y sont les valeurs des écarts verticaux et horizontaux entre les deux points.

public class CalculDist : MasterScript
{
    public ViveControllerInputTest RightController;
    public VRTK_Pointer pointer;
    //public bool boolButton;

    [Header("Prefab des lasers")]
    public GameObject laserPrefab;//préfab des laser contenant des objects tooltip pour l'affichage des valeurs
    public GameObject laserXPrefab;
    public GameObject laserYPrefab;
    public GameObject HitPointSphere; //Matérialisation du point d'impact

    [Header("Réglage des dimensions")]
    public float echelle;
    public float Taille_Sphère;

    [Header("Affichage des valeurs")]//Text pour afficher les valeurs sur un canvas
    public Text AffPreX;
    public Text AffPreY;
    public Text AffPre;



    private GameObject laser; //Laser à instancier
    private GameObject laserX;
    private GameObject laserY;
    private Transform laserTransform;//Cordonnées du laser
    private Transform laserTransformX;
    private Transform laserTransformY;
    private float dist;
    private float distX;
    private float distY;


    private bool firstHit;  //Flag pour la succession de la selection des points.
    private bool secondHit;
    private bool flag;
    private Vector3 hitpoint1;  //Coordonnées des points d'impacts
    private Vector3 hitpoint2;
    private GameObject sphere2; //Matérialisation des points d'impacts
    private GameObject sphere1;



    void Start()
    {
        laser = Instantiate(laserPrefab);//instantiate des lasers
        laserTransform = laser.transform;
        laserX = Instantiate(laserXPrefab);
        laserTransformX = laserX.transform;
        laserY = Instantiate(laserYPrefab);
        laserTransformY = laserY.transform;
        firstHit = false;
        secondHit = false;
        flag = false;
    }

    // Update is called once per frame
    void Update()
    {
        try
        {

            /* switch ((int)Button)
             {
                 case 0:
                     boolButton = RightController.ApplicationMenu;
                     break;
                 case 1:
                     boolButton = RightController.Grip;
                     break;
                 case 2:
                     boolButton = RightController.Touchpad;
                     break;
                 case 3:
                     boolButton = RightController.Trigger;
                     break;
             }*/

            if (firstHit && secondHit)//Actif quand deux tirs ont été réalisé
            {
                firstHit = false;//Remise a zero
                secondHit = false;
                hitpoint1 = new Vector3();
                hitpoint2 = new Vector3();
            }
            if (!boolButton && !flag)//Si on relache le touchpad de la manette
            {
                flag = true;
                if (!firstHit)//si premier tir
                {
                    laser.SetActive(false);//on cache les lasers
                    laserX.SetActive(false);
                    laserY.SetActive(false);
                    DestroyObject(sphere1);//Destruction des anciennes sphères
                    DestroyObject(sphere2);

                    hitpoint1 = pointer.pointerRenderer.GetDestinationHit().point;//point d'impact
                    sphere1 = GameObject.Instantiate(HitPointSphere, hitpoint1, new Quaternion()); // création de la spère au point d'impact
                    sphere1.transform.localScale = new Vector3(Taille_Sphère, Taille_Sphère, Taille_Sphère);//scale de la sphère  
                    firstHit = true;
                    return;
                }
                if (firstHit && !secondHit)//si second tir
                {
                    hitpoint2 = pointer.pointerRenderer.GetDestinationHit().point;// point d'impact
                    sphere2 = GameObject.Instantiate(HitPointSphere, hitpoint2, new Quaternion());// création de la spère au point d'impact
                    sphere2.transform.localScale = new Vector3(Taille_Sphère, Taille_Sphère, Taille_Sphère);//scale de la sphère                             
                    secondHit = true;
                    dist = Vector3.Distance(hitpoint1, hitpoint2);// calcul de la distance entre les deux spères
                    ShowLaser();
                    return;
                }
            }
            if (RightController.Touchpad && flag)//Si on appuie sur le touchpad de la manette
            {
                flag = false;
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning(e.Message);
        }
    }

    private void ShowLaser()
    {
        laser.SetActive(true);//Affichage du laser
        laserTransform.position = Vector3.Lerp(hitpoint1, hitpoint2, .5f);//Coordonnées du point d'origine
        laserTransform.LookAt(hitpoint2);
        laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y, dist);

        Vector3 ThirdPoint; //point servant a afficher les axes vertical et horizontal

        if (hitpoint1.y < hitpoint2.y)//si le deuxième point est plus haut que le premier
        {
            ThirdPoint = hitpoint2;
            ThirdPoint.y = hitpoint1.y; // Le troisième point prend la valeur du point le plus haut avec la hauteur de celui le plus bas 

            distX = Vector3.Distance(ThirdPoint, hitpoint1);
            distY = Vector3.Distance(ThirdPoint, hitpoint2);

            laserX.SetActive(true);//Affichage du laserX
            laserTransformX.position = Vector3.Lerp(hitpoint1, ThirdPoint, .5f);//Coordonnées du point d'origine
            laserTransformX.LookAt(ThirdPoint);
            laserTransformX.localScale = new Vector3(laserTransformX.localScale.x, laserTransformX.localScale.y, distX);

            laserY.SetActive(true);//Affichage du laserY
            laserTransformY.position = Vector3.Lerp(hitpoint2, ThirdPoint, .5f);//Coordonnées du point d'origine
            laserTransformY.LookAt(ThirdPoint);
            laserTransformY.localScale = new Vector3(laserTransformX.localScale.x, laserTransformX.localScale.y, distY);
        }
        else
        {
            ThirdPoint = hitpoint1;
            ThirdPoint.y = hitpoint2.y;// Le troisième point prend la valeur du point le plus haut avec la hauteur de celui le plus bas 

            distX = Vector3.Distance(ThirdPoint, hitpoint2);
            distY = Vector3.Distance(ThirdPoint, hitpoint1);

            laserX.SetActive(true);//Affichage du laserX
            laserTransformX.position = Vector3.Lerp(hitpoint2, ThirdPoint, .5f);//Coordonnées du point d'origine
            laserTransformX.LookAt(ThirdPoint);
            laserTransformX.localScale = new Vector3(laserTransformX.localScale.x, laserTransformX.localScale.y, distX);

            laserY.SetActive(true);//Affichage du laserY
            laserTransformY.position = Vector3.Lerp(hitpoint1, ThirdPoint, .5f);//Coordonnées du point d'origine
            laserTransformY.LookAt(ThirdPoint);
            laserTransformY.localScale = new Vector3(laserTransformX.localScale.x, laserTransformX.localScale.y, distY);
        }

        dist = echelle * dist;//mise à l'échelle
        distX = echelle * distX;
        distY = echelle * distY;

        laser.GetComponentInChildren<VRTK_ObjectTooltip>().UpdateText(dist.ToString());//affichage des mesures sur les lasers
        laserX.GetComponentInChildren<VRTK_ObjectTooltip>().UpdateText(distX.ToString());
        laserY.GetComponentInChildren<VRTK_ObjectTooltip>().UpdateText(distY.ToString());

        AffPre.text = dist.ToString();//affichage des mesures sur le canvas;
        AffPreX.text = distX.ToString();
        AffPreY.text = distY.ToString();
    }
}

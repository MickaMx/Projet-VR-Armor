using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Script servant a calculer la distance entre deux points, a utiliser avec le script RaycastAffichage

public class CalculDist : MonoBehaviour
{
    public Text NomObject;  //affichage de la distance
    public GameObject HitPointSphere; //Matérialisation du point d'impact
    public int echelle;
    public float Taille_Sphère;

    bool firstHit;  //Flag pour la succession de la selection des points.
    bool secondHit;
    Vector3 hitpoint1;  //Coordonnées des points d'impacts
    Vector3 hitpoint2;
    GameObject sphere2; //Matérialisation du point d'impact
    GameObject sphere1;
    // Use this for initialization
    void Start()
    {

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
                DestroyObject(sphere1);//Destruction des anciennes sphères
                DestroyObject(sphere2);
                //Debug.Log("RAZ");
                NomObject.text = "Distance = ";

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
                Debug.DrawLine(hitpoint1, hitpoint2);
                secondHit = true;
                float dist = Vector3.Distance(hitpoint1, hitpoint2);// calcul de la distance entre les deux spères
                dist = echelle * dist;//mise à l'échelle
                NomObject.text += dist;//affichage sur l'UI
                Debug.Log(dist);
                return;
            }
        } 
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaycastAffichage : MonoBehaviour
{
    public GameObject laserPrefab; //Préfab du laser à afficher
    public Vector3 hitPointToSend; // Coordonnées du point d'impact, utiliser dans CalculDist

    private Collider cible; //Object touché par le laser
    private Vector3 hitPoint; //Coordonnées du point d'impact
    private GameObject laser; //Laser à instancier
    private Transform laserTransform;//Cordonnées du laser

    // Use this for initialization
    void Start()
    {
        laser = Instantiate(laserPrefab);   
        laserTransform = laser.transform;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Input.GetMouseButton(1))// si clic gauche
        { 
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, Mathf.Infinity))//Raycast et récupération des coordonnées de l'impact dans la variables hit
            {
                hitPoint = hit.point;//Récupération des coordonnées
                hitPointToSend = hitPoint;//envoie vers CalculDist si besoin
                ShowLaser(hit);//Affichage du laser
            }
        }
        else
        { 
            laser.SetActive(false);//Sinon on cache le laser
        }
    }
    private void ShowLaser(RaycastHit hit)
    {
        laser.SetActive(true);//Affichae du laser
        laserTransform.position = Vector3.Lerp(transform.position, hit.point, .5f);//Coordonnées du point d'origine
        laserTransform.LookAt(hit.point);
        laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y, hit.distance);
    }
}

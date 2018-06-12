using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

//Script servant a calculer la distance entre deux points, a utiliser avec le script RaycastAffichage

public class CalculDist : MonoBehaviour
{
    public GameObject laserPrefab;
    public GameObject HitPointSphere; //Matérialisation du point d'impact
    public float echelle;
    public float Taille_Sphère;
    private SteamVR_TrackedObject trackedObj;
    private GameObject laser; //Laser à instancier
    private Transform laserTransform;//Cordonnées du laser
    private float dist;
    public VRTK_Pointer pointer;
    public VRTK_ControllerTooltips Tooltip;

    public ViveControllerInputTest RightController;



    bool firstHit;  //Flag pour la succession de la selection des points.
    bool secondHit;
    Vector3 hitpoint1;  //Coordonnées des points d'impacts
    Vector3 hitpoint2;
    GameObject sphere2; //Matérialisation du point d'impact
    GameObject sphere1;
    // Use this for initialization

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        trackedObj = RightController.GetComponent<SteamVR_TrackedObject>();
    }

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
        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip))//SLeftController.GripRealeasei clic gauche
        {
            if (!firstHit)//si premier tir
            {
                laser.SetActive(false);
                DestroyObject(sphere1);//Destruction des anciennes sphères
                DestroyObject(sphere2);
                //Debug.Log("RAZ");

                //Debug.Log("First hit");
                hitpoint1 = pointer.pointerRenderer.GetDestinationHit().point;
                //Debug.Log(hitpoint1.ToString());
                //hitpoint1 = GetComponent<RaycastAffichage>().hitPointToSend; // récupération des coordonnées du premier tir
                sphere1 = GameObject.Instantiate(HitPointSphere, hitpoint1,new Quaternion()); // création de la spère au point d'impact
                sphere1.transform.localScale = new Vector3(Taille_Sphère, Taille_Sphère, Taille_Sphère);//scale de la sphère  
                firstHit = true;
                return;
            }
            if (firstHit && !secondHit)//si second tir
            {
                //Debug.Log("Second hit");
                hitpoint2 = pointer.pointerRenderer.GetDestinationHit().point;
                //Debug.Log(hitpoint2.ToString());
                //hitpoint2 = GetComponent<RaycastAffichage>().hitPointToSend;// récupération des coordonnées du premier tir
                sphere2 = GameObject.Instantiate(HitPointSphere, hitpoint2, new Quaternion());// création de la spère au point d'impact
                sphere2.transform.localScale = new Vector3(Taille_Sphère, Taille_Sphère, Taille_Sphère);//scale de la sphère                             
                secondHit = true;
                dist = Vector3.Distance(hitpoint1, hitpoint2);// calcul de la distance entre les deux spères
                ShowLaser();
                Debug.Log(dist.ToString());
                dist = echelle * dist;//mise à l'échelle
                Debug.Log(dist.ToString());
                Tooltip.UpdateText(VRTK_ControllerTooltips.TooltipButtons.TouchpadTooltip, dist.ToString());
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
/* Ce script est utiliser lorsque l'on veut mettre en evidence certaine partie d'un modèle.
La pièce en question reste visible tandis que tout le reste du modèle est transparent.
En plus de cela, des informations sont affichées sur le Tooltip, de base celui ci est situé dans le casque.*/
public class AffichageToolTip : MonoBehaviour
{


    [Header("Affichage des informations")]
    [Tooltip("Tooltip servant à l'affichage")]
    public VRTK_ObjectTooltip Tooltip;
    [Tooltip("information à afficher")]
    public string textAffichage;


    private Material TransparentMaterial;//Matériel transparent
    private GameObject Parent;//Objet initial du modèle
    private Renderer[] allChildren;//Tableau d'objet contenant tout le modèle
    protected Material OriginMaterial;//matériel de la pièce


    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void Awake()
    {
        OriginMaterial = this.GetComponent<MeshRenderer>().material;//récupération du matériel initial
        TransparentMaterial = Resources.Load("Transparent", typeof(Material)) as Material;//récupération du matériel transparent
    }





    public void OnTriggerEnter(Collider other)
    {
        Tooltip.containerColor.a = 255;
        Tooltip.UpdateText(textAffichage);//on affiche les informations
        Parent = transform.root.gameObject;//on recupère le gameobject parent dans la hierarchie
        allChildren = Parent.GetComponentsInChildren<Renderer>();//Récupération des objets du modele


        if (tag == "Highlight")
        {
            tag = "Origin";//tag l'objet d'origin 
            for (int i = 0; i < allChildren.Length; i++)//On parcours tout les objects du modèle
            {
                if (allChildren[i].tag != "Origin")//Si ce n'est pas l'origine on le rend transparent
                {
                    allChildren[i].material = TransparentMaterial;
                }
                if (allChildren[i].tag == "Origin")//Si c'est l'origine on lui rend son material de base.
                {
                    allChildren[i].material = this.OriginMaterial;
                }
            }
            tag = "Highlight";//on remet le bon tag
        }

    }

    public void OnTriggerExit(Collider other)
    {
        for (int i = 0; i < allChildren.Length; i++)//On parcours tout les objets du modèle pour leurs rendre leurs aspects initiales
        {
            allChildren[i].material = allChildren[i].GetComponent<AffichageToolTip>().OriginMaterial;//Remise au matériel initial
        }
    }

    public Material GetOriginMaterial()
    {
        return OriginMaterial;
    }
}

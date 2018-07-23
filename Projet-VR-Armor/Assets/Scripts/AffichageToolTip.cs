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
    public VRTK_ControllerTooltips Tooltip;//Tooltip servant à l'affichage
    public string textAffichage;//information à afficher

    [Header("Mise en valeur")]
    public Material TransparentMaterial;//Matériel transparent


    Material OriginMaterial;//matériel de la pièce
    GameObject Parent;//Objet initial du modèle
    Transform[] allChildren;//Tableau d'objet contenant tout le modèle


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
    }





    public void OnTriggerEnter(Collider other)
    {
        Tooltip.UpdateText(VRTK_ControllerTooltips.TooltipButtons.TriggerTooltip, textAffichage);//on affiche les informations
        Parent = transform.root.gameObject;//on recupère le gameobject parent dans la hierarchie
        allChildren = Parent.GetComponentsInChildren<Transform>();//Récupération des objets du modele


        if (tag == "Highlight")
        {
            tag = "Origin";//tag l'objet d'origin 
            for (int i = 0; i < allChildren.Length; i++)//On parcours tout les objects du modèle
            {

                if (allChildren[i].gameObject.GetComponent<Renderer>() != null)//Si ils on un Renderer
                {
                    if (allChildren[i].tag != "Origin")//Si ce n'est pas l'origine on le rend transparent
                    {
                        allChildren[i].GetComponent<Renderer>().material.shader = Shader.Find("Standard");
                        allChildren[i].GetComponent<Renderer>().material = TransparentMaterial;
                    }
                }
            }
            tag = "Highlight";//on remet le bon tag
        }

    }

    public void OnTriggerExit(Collider other)
    {
        for (int i = 0; i < allChildren.Length; i++)//On parcours tout les objets du modèle pour leurs rendre leurs aspects initiales
        {
            if (allChildren[i].gameObject.GetComponent<Renderer>() != null && allChildren[i].GetComponent<AffichageToolTip>() != null)//Si ils ont un Renderer
            {
                allChildren[i].GetComponent<Renderer>().material = allChildren[i].GetComponent<AffichageToolTip>().OriginMaterial;//Remise au matériel initial
                allChildren[i].GetComponent<Renderer>().material.shader = Shader.Find("Standard");
            }
        }
    }
}

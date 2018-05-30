using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemColliderHandler : MonoBehaviour {

    MeshRenderer mr;
    Color[] originColors; //Devenu un tableau d'origin Colors (car plusieurs matériaux id à gérer désormais)
    public string mainTxtContent;
    public Texture2D imgTex;//Texte à afficher sur l'UI
    public Color callOutColor = Color.red;//Couleur de l'object si on passe la souris dessus
    public Material Transparent;//Matériel transparent
    public Material ToHighlight;//Matériel qui ressort quand le reste est transparent
    GameObject Parent;//Object initial du modèle
    Transform[] allChildren;//Tableau d'object contenant tout le modèle
    Material[] materials;//Tableau de materiel contenant tout les matériel du modèle
    bool[] istransparent;//Flag de la transparence


    // Use this for initialization
    void Start () {
        Parent = GameObject.FindGameObjectWithTag("Model");//Trouver object initial, qui doit avoir le tag Model
        allChildren = Parent.GetComponentsInChildren<Transform>();//Récupération des objects du modele
        istransparent = new bool[allChildren.Length]; //Initailisation des flags
        for (int j = 0; j < istransparent.Length; j++)//
        {
            istransparent[j] = false;
        }
        materials = new Material[allChildren.Length];//Initialisation des matériels
        for (int i = 0; i < allChildren.Length; i++) //
        {
            if (allChildren[i].gameObject.GetComponent<Renderer>() != null)//Si il a un Renderer
            {
                Renderer tmp = allChildren[i].GetComponent<Renderer>();
                materials[i] = tmp.material;//On récupère le materiel
            }          
        }

            mr = GetComponent<MeshRenderer>();//Récuération du matériel de l'object
        originColors = new Color[mr.materials.Length]; // on initialise la longueur du tableau d'origin Colors
        for (int i = 0; i < mr.materials.Length; i++) //méthode "for" pour "cycler" l'ensemble des matériaux du MeshRenderer (il existe aussi foreach)
        {
            if (mr.materials[i].HasProperty("_Color"))
                originColors[i] = mr.materials[i].color;
            else
                originColors[i] = Color.white; // on rempli (inutilement) le tableau avec une valeur Blanche, dans le cas ou le matériau en question n'a pas de "Color"
        }

	}
	
	// Update is called once per frame
	void OnMouseDown () {//Si clic
        tag = "Origin";//tag l'object d'origin 
        for(int i=0; i < allChildren.Length; i++)//On parcours tout les objects du modèle
        {
            if(allChildren[i].gameObject.GetComponent<Renderer>() != null)//Si ils on un Renderer
            {
                if(allChildren[i].tag!= "Origin")//Si ce n'est pas l'origine
                {
                    Renderer rd = allChildren[i].gameObject.GetComponent<Renderer>();
                    if (istransparent[i])//Si il est transparent
                    {
                        allChildren[i].GetComponent<Renderer>().material = materials[i];                    //
                        allChildren[i].GetComponent<Renderer>().material.shader = Shader.Find("Standard");  //Remise au matériel initial
                        allChildren[i].GetComponent<Renderer>().material.SetFloat("_Mode", 2);              //
                        istransparent[i] = false;                                                           //
                        Parent.GetComponent<IsOnFocus>().isOnFocus = false;                                 //
                    }
                    else//Si opaque
                    {
                        allChildren[i].GetComponent<Renderer>().material.shader= Shader.Find("Legacy Shaders/Transparent/Diffuse");//On met le matériel à transparent
                        allChildren[i].GetComponent<Renderer>().material = Transparent;
                        istransparent[i] = true;
                        Parent.GetComponent<IsOnFocus>().isOnFocus = true;
                    }
                }
                else//Si c'est l'object d'origin
                {
                    Renderer rd = allChildren[i].gameObject.GetComponent<Renderer>();
                    if (istransparent[i])//Remise au matériel initial
                    {
                        allChildren[i].GetComponent<Renderer>().material = materials[i];
                        allChildren[i].GetComponent<Renderer>().material.shader = Shader.Find("Standard");
                        allChildren[i].GetComponent<Renderer>().material.SetFloat("_Mode", 2);
                        istransparent[i] = false;
                        Parent.GetComponent<IsOnFocus>().isOnFocus = false;
                    }
                    else//Changement du matériel par celui prévue 
                    {
                        allChildren[i].GetComponent<Renderer>().material.shader = Shader.Find("Legacy Shaders/Transparent/Diffuse");
                        allChildren[i].GetComponent<Renderer>().material = ToHighlight;
                        istransparent[i] = true;
                        Parent.GetComponent<IsOnFocus>().isOnFocus = true;
                    }
                }
            }
        }
        tag = "Untagged";
    }


    void OnMouseEnter()
    {
        if(!Parent.GetComponent<IsOnFocus>().isOnFocus)//Si le modèle n'est pas transparent
        {
            for (int i = 0; i < mr.materials.Length; i++)
            if (mr.materials[i].HasProperty("_Color")) // on check le material, car certains n'ont pas d'attribut "Color"
                mr.materials[i].SetColor("_Color", callOutColor);
        }
    }

    void OnMouseExit()
    {
        if (!Parent.GetComponent<IsOnFocus>().isOnFocus)//Si le modèle n'est pas transparent
        {
            for (int i = 0; i < mr.materials.Length; i++)
                if (mr.materials[i].HasProperty("_Color")) // on check le material, car certains n'ont pas d'attribut "Color"
                    mr.materials[i].SetColor("_Color", originColors[i]); // on restaure la bonne originColor correspondante
        }                   
    }
}

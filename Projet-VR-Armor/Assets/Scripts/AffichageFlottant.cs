using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AffichageFlottant : MonoBehaviour {

    public string calloutLabel;
    MeshRenderer mr;
    float height;
    public float extraYOffset = 0.3f;
    public Color callOutColor = Color.blue;

    // Use this for initialization
    void Start()
    {

        // PERMET D'OBTENIR LA HEIGHT (en world) DE L'OBJET
        mr = GetComponent<MeshRenderer>();
        height = mr.bounds.size.y;
        //UIManager.Instance.DisplayCallOut(gameObject, calloutLabel, height, extraYOffset, callOutColor);
    }

    void Update()
    {
        //UIManager.Instance.DisplayCallOut(gameObject, calloutLabel, height, extraYOffset, callOutColor);
    }

   /* void OnMouseExit()
    {
        UIManager.Instance.HideCallOut();
    }*/
}

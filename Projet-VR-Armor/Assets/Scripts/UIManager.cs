using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    //CET OBJET EST UN SINGLETON : ON POURRA Y ACCEDER PLUS FACILEMENT DE L'EXTERIEUR
    private static UIManager _instance;
    public static UIManager Instance
    {
        get { if (!_instance)
               _instance = FindObjectOfType<UIManager>();
            return _instance;  }
    }
  
    public RectTransform panelCallout;
    //public RectTransform panelFicheTechnique;
    public GameObject BtRetourAccueil;

    //public MainColliderHandler[] mainColliderHandlers;

    void Awake()
    {
        _instance = this;
    }

    // Use this for initialization
    void Start () {

        if (SceneManager.GetActiveScene().name.Contains("Accueil"))
            BtRetourAccueil.SetActive(false);

        HideCallOut();
        //HidePanelFicheTechnique();
        //mainColliderHandlers = FindObjectsOfType<MainColliderHandler>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GotoScene0()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitApp()
    {
        Application.Quit();
    }

    public void DisplayCallOut(GameObject callOutGo, string label, float height, float extraOffset, Color bgColor)
    {
        Vector3 pos2D;
        // ON CONVERTI LA WORLD POSITION XYZ DU MAINCOLLIDER EN 2D (en y rajoutant sa hauteur + un delta de extraOffset)
        pos2D = MySceneManager.Instance.currentActiveCam.WorldToScreenPoint
            (callOutGo.GetComponent<Transform>().position
            + Vector3.up * height
            + Vector3.up * extraOffset);

        panelCallout.position = pos2D;
        panelCallout.gameObject.SetActive(true);
        panelCallout.sizeDelta = new Vector2(label.Length * 6 + 50, 40);
        panelCallout.GetComponentInChildren<Text>().text = label;
        panelCallout.GetComponent<Image>().color = bgColor;
    }

    public void HideCallOut()
    {
        panelCallout.gameObject.SetActive(false);
    }

    /*public void DisplayFicheTechnique(string text, Texture2D mainTex)
    {
        panelFicheTechnique.gameObject.SetActive(true);
        panelFicheTechnique.GetComponentInChildren<Text>().text = text;
        panelFicheTechnique.GetComponentInChildren<RawImage>().texture = mainTex;
    }

    public void HidePanelFicheTechnique()
    {
        panelFicheTechnique.gameObject.SetActive(false);
    }*/
}

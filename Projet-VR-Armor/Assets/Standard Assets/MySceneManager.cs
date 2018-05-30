using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityStandardAssets.Characters.FirstPerson;

public class MySceneManager : MonoBehaviour {

    //CET OBJET EST UN SINGLETON : ON POURRA Y ACCEDER PLUS FACILEMENT DE L'EXTERIEUR
    private static MySceneManager _instance;
    public static MySceneManager Instance
    {
        get
        {
            if (!_instance)
                _instance = FindObjectOfType<MySceneManager>();
            return _instance;
        }
    }
    public string mySceneName;
    bool isFPSView = false;
        
    GameObject thirdPersonGo;
    GameObject thirdPersonCameraGo;
    GameObject cam360Go;
    GameObject cam360_2Go;
    GameObject cam360_3Go;
    GameObject cam360_4Go;
    GameObject cam360_5Go;

    Camera thirdPersonCam;
    Camera cam360;
    Camera cam360_2;
    Camera cam360_3;
    Camera cam360_4;
    Camera cam360_5;

	public AnimationCurve lerpCurve = new AnimationCurve();
	[Header("Private")]
    public Camera currentActiveCam;
	public Camera currentActiveCamLastFrame;
	private Camera tmpCam;
	public float lerpSpeed = 1f;
	public float prct;

	

	void Awake()
    {
        _instance = this;
        // MOYEN DE TROUVER UN GAMEOBJECT PAR NOM
        // thirdPersonGo = GameObject.Find("Player_Woman_EPI");

        // MOYEN DE TROUVER UN GAMEOBJECT PAR TAG
        thirdPersonGo = GameObject.FindGameObjectWithTag("Player");
        thirdPersonCameraGo = GameObject.Find("3rdPersonCamera_FreeFormAndTargeting");
        thirdPersonCam = thirdPersonCameraGo.GetComponent<Camera>();

        cam360Go = GameObject.Find("Camera360");
        cam360 = cam360Go.GetComponent<Camera>();

        cam360_2Go = GameObject.Find("Camera360_2");
        cam360_2 = cam360_2Go.GetComponent<Camera>();

        cam360_3Go = GameObject.Find("Camera360_3");
        cam360_3 = cam360_3Go.GetComponent<Camera>();

        cam360_4Go = GameObject.Find("Camera360_4");
        cam360_4 = cam360_4Go.GetComponent<Camera>();

        cam360_5Go = GameObject.Find("Camera360_5");
        cam360_5 = cam360_5Go.GetComponent<Camera>();

        SetInstant3rdView();

        //  Debug.Log("JE SUIS DANS LE AWAKE");
    }

    // Use this for initialization
    void Start ()
    {
        // Debug.Log("JE SUIS DANS LE START");
        //currentActiveCam = thirdPersonCam;

    }
	
	// Update is called once per frame
	void Update ()
    {

		//if (Input.GetKeyDown(KeyCode.P))
		//    ToggleViewMode();

		if (currentActiveCam != currentActiveCamLastFrame)
		{
			StopAllCoroutines();
			StartCoroutine(CamLerp(currentActiveCamLastFrame, currentActiveCam, lerpSpeed));
		}
		currentActiveCamLastFrame = currentActiveCam;
	}

	public IEnumerator CamLerp(Camera from, Camera to, float time)
	{
		if (from)
		{
			float startTime = Time.time;

			Camera tmpCam2 = tmpCam;
			tmpCam = GameObject.Instantiate(tmpCam!=null ? tmpCam : from) as Camera;
			if(tmpCam2)
				Destroy(tmpCam2.gameObject);
			tmpCam.depth = to.depth + 1;
			tmpCam.gameObject.SetActive(true);

			MonoBehaviour[] ms = tmpCam.GetComponents<MonoBehaviour>();
			for (int i = 0; i < ms.Length; i++)
			{
				ms[i].enabled = false;
			}

			prct = (Time.time - startTime) / time;
			while (prct <= 1)
			{
				prct = (Time.time - startTime) / time;

				if (lerpCurve.keys.Length > 0)
					prct = lerpCurve.Evaluate(prct);

				tmpCam.transform.position = Vector3.Lerp(from.transform.position, to.transform.position, prct);
				tmpCam.transform.rotation = Quaternion.Slerp(from.transform.rotation, to.transform.rotation, prct);
				tmpCam.fieldOfView = Mathf.Lerp(from.fieldOfView, to.fieldOfView, prct);

				yield return null;
			}

			Destroy(tmpCam.gameObject);
		}

	}
       
    //void ToggleViewMode()
    //{
    //    if (isFPSView)
    //        SetInstant3rdView();
    //    else
    //        SetInstantFPSView();


    //    isFPSView = !isFPSView;
    //}

    //public void SetInstantFPSView()
    //{
    //    playerOP.SetActive(false);
    //    thirdPersonGo.SetActive(false);
    //    cam360.gameObject.SetActive(false);
    //    currentActiveCam = fpsCam;
    //}

    public void SetInstant3rdView()
    {
        thirdPersonGo.SetActive(true);
        thirdPersonCameraGo.SetActive(true);
        cam360Go.SetActive(false);
        cam360_2Go.SetActive(false);
        cam360_3Go.SetActive(false);
        cam360_4Go.SetActive(false);
        cam360_5Go.SetActive(false);
        currentActiveCam = thirdPersonCam;
    }
 
    public void SetCam360()
    {
        thirdPersonGo.SetActive(true);
        thirdPersonCameraGo.SetActive(true);
        cam360_2Go.SetActive(false);
        cam360_3Go.SetActive(false);
        cam360_4Go.SetActive(false);
        cam360_5Go.SetActive(false);

        cam360Go.SetActive(true);
        currentActiveCam = cam360;
    }

    public void SetCam360_2()
    {
        thirdPersonGo.SetActive(true);
        thirdPersonCameraGo.SetActive(true);
        cam360Go.SetActive(false);
        cam360_3Go.SetActive(false);
        cam360_4Go.SetActive(false);
        cam360_5Go.SetActive(false);

        cam360_2Go.SetActive(true);
        currentActiveCam = cam360_2;
    }

    public void SetCam360_3()
    {
        thirdPersonGo.SetActive(true);
        thirdPersonCameraGo.SetActive(true);
        cam360Go.SetActive(false);
        cam360_2Go.SetActive(false);
        cam360_4Go.SetActive(false);
        cam360_5Go.SetActive(false);


        cam360_3Go.SetActive(true);
        currentActiveCam = cam360_3;
    }

    public void SetCam360_4()
    {
        thirdPersonGo.SetActive(false);
        thirdPersonCameraGo.SetActive(false);
        cam360Go.SetActive(false);
        cam360_2Go.SetActive(false);
        cam360_3Go.SetActive(false);
        cam360_5Go.SetActive(false);


        cam360_4Go.SetActive(true);
        currentActiveCam = cam360_4;
    }

    public void SetCam360_5()
    {
        thirdPersonGo.SetActive(false);
        thirdPersonCameraGo.SetActive(false);
        cam360Go.SetActive(false);
        cam360_2Go.SetActive(false);
        cam360_3Go.SetActive(false);
        cam360_4Go.SetActive(false);


        cam360_5Go.SetActive(true);
        currentActiveCam = cam360_5;
    }
}

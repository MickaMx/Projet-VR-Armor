using System.Collections;
using UnityEngine;

public class ViveControllerInputTest : MonoBehaviour
{
    [Header("Bools accessibles aux Listeners")]
    [Tooltip("Bool pour le bouton Application")]
    public bool ApplicationMenu;
    [Tooltip("Bool pour le bouton Grip")]
    public bool Grip;
    [Tooltip("Bool pour le Touchpad")]
    public bool Touchpad;
    [Tooltip("Bool pour la gachette")]
    public bool Trigger;

    [Header("Script utilisant les bools comme Input")]
    [Tooltip("Tableau de script dérivant de MasterScript")]
    public MasterScript[] Listeners;

    private SteamVR_TrackedObject trackedObj;
    private int Length;

    public enum Boutton
    {
        ApplicationMenu,
        Grip,
        Touchpad,
        Trigger
    };


    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        Length = Listeners.Length;
    }

    private void Update()
    {

        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            Grip = true;
            StartCoroutine("ChangeInput");
        }

        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
        {
            Grip = false;
            StartCoroutine("ChangeInput");
        }

        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            Trigger = true;
            StartCoroutine("ChangeInput");
        }

        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            Trigger = false;
            StartCoroutine("ChangeInput");
        }

        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Touchpad = true;
            StartCoroutine("ChangeInput");
        }

        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Touchpad = false;
            StartCoroutine("ChangeInput");
        }

        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            ApplicationMenu = true;
            StartCoroutine("ChangeInput");
        }

        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            ApplicationMenu = false;
            StartCoroutine("ChangeInput");
        }

       

    }
    IEnumerator ChangeInput()
    {
        for (int i = 0; i < Length; i++)
        {
            switch ((int)Listeners[i].Button)
            {
                case 0:
                    Listeners[i].boolButton = ApplicationMenu;
                    break;
                case 1:
                    Listeners[i].boolButton = Grip;
                    break;
                case 2:
                    Listeners[i].boolButton = Touchpad;
                    break;
                case 3:
                    Listeners[i].boolButton = Trigger;
                    break;
            }
        }
        yield return null;
    }
}

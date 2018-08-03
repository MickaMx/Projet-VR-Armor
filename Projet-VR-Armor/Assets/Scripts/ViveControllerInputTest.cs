/*
 * Copyright (c) 2016 Razeware LLC
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System.Collections;
using UnityEngine;

public class ViveControllerInputTest : MonoBehaviour
{
    public bool ApplicationMenu;
    public bool Grip;
    public bool Touchpad;
    public bool Trigger;

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

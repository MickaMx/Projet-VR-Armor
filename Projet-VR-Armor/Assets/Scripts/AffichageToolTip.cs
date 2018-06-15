using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class AffichageToolTip : MonoBehaviour {
    public VRTK_ControllerTooltips Tooltip;
    //public VRTK_ObjectTooltip Tooltip;
    public string textAffichage;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter(Collider other)
    {
        Tooltip.UpdateText(VRTK_ControllerTooltips.TooltipButtons.TriggerTooltip, textAffichage);
    }

}

namespace VRTK.Examples
{
    using UnityEngine;
    using UnityEventHelper;

    public class ButtonReactor : MonoBehaviour //joue deux animation en alternance a chaque appuie sur le bouton
    {
        public GameObject objectToAnimate;
        public string Animation1;
        public string Animation2;


        private bool firstHit;  //Flag pour la succession de la selection des points.
        private bool secondHit;
        private Animation anim;

        private VRTK_Button_UnityEvents buttonEvents;

        private void Start()
        {
            buttonEvents = GetComponent<VRTK_Button_UnityEvents>();
            if (buttonEvents == null)
            {
                buttonEvents = gameObject.AddComponent<VRTK_Button_UnityEvents>();
            }
            buttonEvents.OnPushed.AddListener(handlePush);
        }

        private void handlePush(object sender, Control3DEventArgs e)
        {
                if (firstHit && secondHit)//Actif quand deux tirs ont été réalisé
                {
                    firstHit = false;//Remise a zero
                    secondHit = false;
                }
                if (!firstHit)//si premier appui
                {
                    // Debug.Log("play ouverture");
                    firstHit = true;
                    anim.Play(Animation1);
                    return;
                }
                if (firstHit && !secondHit)//si second appui
                {
                    // Debug.Log("play Fermeture");
                    secondHit = true;
                    anim.Play(Animation2);
                    return;
                } 
        }
    }
}
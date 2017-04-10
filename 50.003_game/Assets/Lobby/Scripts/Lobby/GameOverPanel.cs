using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Prototype.NetworkLobby
{
    public class GameOverPanel : MonoBehaviour
    {
        public bool isInGame = false;

        public bool isDisplayed = false;
        protected Image panelImage;
        public Text infoText;
        public Text buttonText;
        public Button singleButton;

        void Start()
        {
            panelImage = GetComponent<Image>();
            ToggleVisibility(isDisplayed);
        }

        public void ToggleVisibility(bool visible)
        {
            isDisplayed = visible;
            foreach (Transform t in transform)
            {
                //Access all the children of a transform 
                t.gameObject.SetActive(isDisplayed);
            }

            if (panelImage != null)
            {
                panelImage.enabled = isDisplayed;
            }
        }
    }
}
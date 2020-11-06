using UnityEngine;
using UnityEngine.UI;

namespace Playcraft.Examples.SceneControl
{
    public class LoadingPanel : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] Image background;
        [SerializeField] Text description;
        #pragma warning restore 0649
        
        public void Display(SceneTransitionSO transition)
        {
            description.text = transition.displayMessage;
            SetVisible(true);
        }
        
        public void SetVisible(bool value)
        {
            background.enabled = value;
            description.enabled = value;
        }
    }
}

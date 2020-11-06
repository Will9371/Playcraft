using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Playcraft.Examples.SceneControl
{
    public class LoadingPanel : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] Image background;
        [SerializeField] Text description;
        #pragma warning restore 0649
        
        SceneTransitionSO transition;
        public void StoreTransition(SceneTransitionSO value) { transition = value; }
        
        public void Display() { Display(transition); }
        public void Display(SceneTransitionSO value)
        {
            description.text = value.displayMessage;
            SetVisible(true);
        }
        
        public void SetVisible(bool value)
        {
            background.enabled = value;
            description.enabled = value;
        }
    }
}

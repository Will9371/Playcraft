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
        
        SceneTransitionSOWithString transition;

        public void StoreTransition(SceneTransitionSOWithString value) { transition = value; }
        
        public void Display() { Display(transition); }
        public void Display(SceneTransitionSO value)
        {
            if (value is SceneTransitionSOWithString stringValue)
                Display(stringValue);
            else
                Debug.LogError(value + " cannot be cast to SceneTransitionSOWithString");
        }
        public void Display(SceneTransitionSOWithString value)
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

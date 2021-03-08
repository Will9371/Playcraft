using UnityEngine;
using UnityEngine.UI;
using Playcraft.Scene;

namespace Playcraft.Examples.SceneControl
{
    public class LoadingPanel : MonoBehaviour
    {
        [SerializeField] Image background;
        [SerializeField] Text description;
        
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

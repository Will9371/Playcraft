using UnityEngine;

namespace Playcraft.Scene
{
    public class AccessAdditiveSceneController : MonoBehaviour
    {
        AdditiveSceneController scene => AdditiveSceneController.instance;
        
        SceneTransitionSO transition;
        public void StoreTransition(SceneTransitionSO value) { transition = value; }
        public void Transition() { scene.LoadUnload(transition); }    
    }
}

using UnityEngine;
using twicebetter;

namespace Playcraft
{
    public class FocusedSceneSOInterface : ScriptableObject, IRelayBool
    {
        public virtual void Relay(bool value) { }
    }
    
    [CreateAssetMenu(menuName = "Playcraft/Editor/Set Focused Scene")]
    public class SetStartFocusedSceneSO : FocusedSceneSOInterface
    {
        public bool startInScene;
        public bool disabled;
        
        void OnValidate() { Relay(startInScene); }
        
        public override void Relay(bool newValue) 
        {
            startInScene = newValue;
            KeepSceneFocused.forceFocusSceneOnPlay = newValue; 
            
            KeepSceneFocused.disabled = disabled;
        }
    }
}


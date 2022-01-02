using UnityEngine;

namespace Playcraft
{
    [CreateAssetMenu(menuName = "Playcraft/Input/Keybindings")]
    public class Keybindings : ScriptableObject
    {
        public Keybinding[] values;
        
        public Keybinding GetBinding(SO id)
        {
            foreach (var value in values)
                if (value.id == id)
                    return value;
                    
            return null;
        }
    }
}

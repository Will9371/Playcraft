using UnityEngine;

namespace ZMD
{
    [CreateAssetMenu(menuName = "ZMD/Input/Keybindings")]
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

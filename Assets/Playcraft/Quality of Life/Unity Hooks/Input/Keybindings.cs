using UnityEngine;

namespace Playcraft
{
    [CreateAssetMenu(menuName = "Playcraft/Input/Keybindings")]
    public class Keybindings : ScriptableObject
    {
        public Keybinding[] values;
    }
}

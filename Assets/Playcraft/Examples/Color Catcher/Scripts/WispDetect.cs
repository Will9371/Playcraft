using UnityEngine;

namespace Playcraft.Examples.ColorCatcher
{
    public class WispDetect : MonoBehaviour
    {
        [HideInInspector] public WispType type;
        [SerializeField] BoolEvent Output = default;

        public void Input(Collider other)
        {
            var wisp = other.GetComponent<WispTag>();
            if (!wisp) return;
            //other.enabled = false;
            var success = wisp.value == type;
            Output.Invoke(success);
        }
    }
}

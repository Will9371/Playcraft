using UnityEngine;

namespace Playcraft
{
    namespace Examples
    {
        namespace Willowisp
        {
            public class WispDetect : MonoBehaviour
            {
                [HideInInspector] public WispType type;
                [SerializeField] BoolEvent Output;

                public void Input(Collider other)
                {
                    other.enabled = false;
                    var wisp = other.GetComponent<WispTag>();
                    if (!wisp) return;
                    var success = wisp.value == type;
                    Output.Invoke(success);
                }
            }
        }
    }
}

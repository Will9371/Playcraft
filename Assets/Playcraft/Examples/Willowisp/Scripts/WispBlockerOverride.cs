using UnityEngine;

namespace Playcraft
{
    namespace Examples
    {
        namespace Willowisp
        {
            public class WispBlockerOverride : MonoBehaviour
            {
                public BlockerData data;
                
                Renderer rend;
                WispDetect detect;
                
                private void Awake()
                {
                    rend = GetComponent<Renderer>();
                    detect = GetComponent<WispDetect>();
                    
                    Set();
                }
                
                public void Set(BlockerData value)
                {
                    data = value;
                    Set();
                }
                
                public void Set()
                {
                    rend.material = data.material;
                    detect.type = data.type;
                }
            }
        }
    }
}

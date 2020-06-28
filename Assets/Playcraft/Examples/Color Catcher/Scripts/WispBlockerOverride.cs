using UnityEngine;

namespace Playcraft.Examples.ColorCatcher
{
    public class WispBlockerOverride : MonoBehaviour
    {
        public BlockerData data;
        
        Renderer rend;
        WispDetect detect;
        public MoveBlocker move { get; private set; }
        
        private void Awake()
        {
            rend = GetComponent<Renderer>();
            detect = GetComponent<WispDetect>();
            move = GetComponent<MoveBlocker>();
            
            data.restPosition = transform.position;
            Reset();
        }
        
        public void Set(BlockerData value)
        {
            rend.material = value.material;
            detect.type = value.type;
        }
        
        public void Reset()
        {
            rend.material = data.material;
            detect.type = data.type;
        }
    }
}

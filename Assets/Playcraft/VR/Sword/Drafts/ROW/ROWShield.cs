using System.Linq;
using UnityEngine;

namespace Playcraft.Examples.SwordTrainer
{
    public class ROWShield : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] new Renderer renderer;
        [SerializeField] Vector3SO[] threats;
        #pragma warning restore 0649
        
        public void Input(SwingState value) { Input(value.direction); }
    
        public void Input(Vector3SO otherDestination)
        {
            var isThreat = threats.Contains(otherDestination);
            renderer.enabled = isThreat;
        }
    }
}

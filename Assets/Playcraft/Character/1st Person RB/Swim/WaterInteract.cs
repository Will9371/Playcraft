using UnityEngine;
using UnityEngine.Events;

namespace Playcraft
{
    public class WaterInteract : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] UnityEvent EnterWater;
        [SerializeField] UnityEvent ExitWater;
        #pragma warning restore 0649

        public void RequestEnter(Collider other)
        {
            if (IsWater(other)) 
                EnterWater.Invoke();
        }
        
        public void RequestExit(Collider other)
        {
            if (IsWater(other)) 
                ExitWater.Invoke();        
        }
        
        private bool IsWater(Collider other)
        {
            return other.GetComponent<WaterTag>();
        }
    }
}

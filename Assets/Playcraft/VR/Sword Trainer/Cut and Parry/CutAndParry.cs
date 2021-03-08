using UnityEngine;

namespace Playcraft.Examples.SwordTrainer
{
    public class CutAndParry : MonoBehaviour
    {
        [SerializeField] GetPercentOverTime cutPrompt;
        [SerializeField] SetParry parryPrompt;
        
        void Start() { cutPrompt.Begin(); }
        
        public void CutComplete() { parryPrompt.SetRandomParry(); }
        
        public void ParryComplete() { cutPrompt.Begin(); }
    }
}

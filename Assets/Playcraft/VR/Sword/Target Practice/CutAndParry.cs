using UnityEngine;

namespace Playcraft.Examples.SwordTrainer
{
    public class CutAndParry : MonoBehaviour
    {
        [SerializeField] GetPercentOverTime cutPrompt;
        [SerializeField] Relay parryPrompt;
        
        void Start() { cutPrompt.Begin(); }
        
        public void CutComplete() { parryPrompt.Input(); }
        
        public void ParryComplete() { cutPrompt.Begin(); }
    }
}

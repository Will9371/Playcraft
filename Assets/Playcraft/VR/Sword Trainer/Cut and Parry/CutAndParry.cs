using UnityEngine;

namespace Playcraft.Examples.SwordTrainer
{
    public class CutAndParry : MonoBehaviour
    {
        [SerializeField] CutTarget cutPrompt;
        [SerializeField] SetParry parryPrompt;
        
        void Start() { cutPrompt.SetRandomCut(); }
        
        public void CutInProgress() { parryPrompt.SetRandomParry(false); }
        
        public void CutComplete() { parryPrompt.SetParryReady(); }
        
        public void ParryComplete() { cutPrompt.SetRandomCut(); }
    }
}

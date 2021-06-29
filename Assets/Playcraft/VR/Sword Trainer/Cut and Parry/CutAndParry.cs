using UnityEngine;

namespace Playcraft.Examples.SwordTrainer
{
    public class CutAndParry : MonoBehaviour
    {
        [SerializeField] CutTarget cutPrompt;
        [SerializeField] SetParry parryPrompt;
        
        void Start() { cutPrompt.SetRandomCut(); }
        
        public void CutComplete() { parryPrompt.SetRandomParry(); }
        
        public void ParryComplete() { cutPrompt.SetRandomCut(); }
    }
}

using UnityEngine;

namespace Playcraft.Examples.SwordTrainer
{
    public class CutAndParry : MonoBehaviour
    {
        enum State { Cut, Parry }
        State state;
    
        [SerializeField] CutTarget cutPrompt;
        [SerializeField] SetParry parryPrompt;

        void Start() 
        {
            state = State.Cut; 
            cutPrompt.SetRandomCut(); 
        }
        
        public void CutInProgress() 
        {
            if (state != State.Cut) return; 
            parryPrompt.SetRandomParry(false); 
        }
        
        public void CutComplete() 
        {
            if (state != State.Cut) return;
            state = State.Parry; 
            parryPrompt.SetParryReady();
        }
        
        public void ParryComplete() 
        { 
            if (state != State.Parry) return;
            state = State.Cut;
            cutPrompt.SetRandomCut(); 
        }
    }
}

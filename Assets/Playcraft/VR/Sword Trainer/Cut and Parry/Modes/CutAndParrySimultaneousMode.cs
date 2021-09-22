namespace Playcraft.Examples.SwordTrainer
{
    public class CutAndParrySimultaneousMode : ISwordMode
    {
        SwordTrainer controller;
        public CutAndParrySimultaneousMode(SwordTrainer controller) { this.controller = controller; }
        
        bool isCutComplete;
        bool isParryComplete;
        
        public void Enter() 
        { 
            isCutComplete = true;
            isParryComplete = true;
            RequestAction();
        }
        
        public void Exit() { }

        public void CutComplete() 
        { 
            isCutComplete = true;
            RequestAction();
        }
        
        public void ParryComplete() 
        { 
            isParryComplete = true; 
            RequestAction();
        }
        
        void RequestAction()
        {
            if (!isCutComplete || !isParryComplete)
                return;

            controller.Cut();
            controller.Parry();
            
            isCutComplete = false;
            isParryComplete = false;          
        }
    }
}

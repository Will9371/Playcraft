namespace Playcraft.Examples.SwordTrainer
{
    public class CutAndParrySimultaneousMode : ISwordMode
    {
        SwordTrainer controller;
        public CutAndParrySimultaneousMode(SwordTrainer controller) { this.controller = controller; }
        
        public void Enter() 
        { 
            controller.Cut(0); 
            controller.Parry(0); 
        }

        public void Exit() { }
        public void CutComplete(int index) { controller.Cut(index); }
        public void ParryComplete(int index) { controller.Parry(index); }
    }
}

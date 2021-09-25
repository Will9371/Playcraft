namespace Playcraft.Examples.SwordTrainer
{
    public class CutAndParryMode : ISwordMode
    {
        SwordTrainer controller;
        public CutAndParryMode(SwordTrainer controller) { this.controller = controller; }
        
        public void Enter() { controller.Parry(0); }
        public void Exit() { }
        public void CutComplete(int index) { controller.Parry(0); }
        public void ParryComplete(int index) { controller.Cut(0); }
    }
}

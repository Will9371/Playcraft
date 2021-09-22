namespace Playcraft.Examples.SwordTrainer
{
    public class CutAndParryMode : ISwordMode
    {
        SwordTrainer controller;
        public CutAndParryMode(SwordTrainer controller) { this.controller = controller; }
        
        public void Enter() { controller.Parry(); }
        public void Exit() { }
        public void CutComplete() { controller.Parry(); }
        public void ParryComplete() { controller.Cut(); }
    }
}

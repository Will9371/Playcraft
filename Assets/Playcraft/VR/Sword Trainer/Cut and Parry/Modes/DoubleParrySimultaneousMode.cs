namespace Playcraft.Examples.SwordTrainer
{
    public class DoubleParrySimultaneousMode : ISwordMode
    {
        SwordTrainer controller;
        public DoubleParrySimultaneousMode(SwordTrainer controller) { this.controller = controller; }

        public void Enter() { controller.ParryAll(); }
        public void Exit() { }
        public void CutComplete(int index) { }
        public void ParryComplete(int index) { controller.Parry(index); }
    }
}
namespace Playcraft.Examples.SwordTrainer
{
    public class ParryOnlyMode : ISwordMode
    {
        SwordTrainer controller;
        public ParryOnlyMode(SwordTrainer controller) { this.controller = controller; }

        public void Enter() { controller.Parry(0); }
        public void Exit() { }
        public void CutComplete(int index) { }
        public void ParryComplete(int index) { controller.Parry(0); }
    }
}

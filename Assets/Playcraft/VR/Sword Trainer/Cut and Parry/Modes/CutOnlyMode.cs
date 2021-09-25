namespace Playcraft.Examples.SwordTrainer
{
    public class CutOnlyMode : ISwordMode
    {
        SwordTrainer controller;
        public CutOnlyMode(SwordTrainer controller) { this.controller = controller; }

        public void Enter() { controller.Cut(0); }
        public void Exit() { }
        public void CutComplete(int index) { controller.Cut(0); }
        public void ParryComplete(int index) { }
    }
}
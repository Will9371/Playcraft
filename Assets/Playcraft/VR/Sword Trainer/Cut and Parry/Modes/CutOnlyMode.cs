namespace Playcraft.Examples.SwordTrainer
{
    public class CutOnlyMode : ISwordMode
    {
        SwordTrainer controller;
        public CutOnlyMode(SwordTrainer controller) { this.controller = controller; }

        public void Enter() { controller.Cut(); }
        public void Exit() { }
        public void CutComplete() { controller.Cut(); }
        public void ParryComplete() { }
    }
}
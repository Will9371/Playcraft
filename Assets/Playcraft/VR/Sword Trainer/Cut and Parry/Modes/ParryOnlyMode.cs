namespace Playcraft.Examples.SwordTrainer
{
    public class ParryOnlyMode : ISwordMode
    {
        SwordTrainer controller;
        public ParryOnlyMode(SwordTrainer controller) { this.controller = controller; }

        public void Enter() { controller.Parry(); }
        public void Exit() { }
        public void CutComplete() { }
        public void ParryComplete() { controller.Parry(); }
    }
}

namespace Playcraft.Examples.SwordTrainer
{
    public class DoubleCutSimultaneousMode : ISwordMode
    {
        SwordTrainer controller;
        public DoubleCutSimultaneousMode(SwordTrainer controller) { this.controller = controller; }

        public void Enter() { Cut(); }
        public void Exit() { }
        public void CutComplete() { Cut(); }
        public void ParryComplete() { }
        
        void Cut()
        {
            controller.Cut();
            controller.Cut2();
        }
    }
}
namespace Playcraft.Examples.SwordTrainer
{
    public class DoubleCutSimultaneousMode : ISwordMode
    {
        SwordTrainer controller;
        public DoubleCutSimultaneousMode(SwordTrainer controller) { this.controller = controller; }

        public void Enter() { controller.CutAll(); }
        
        public void Exit() { }
        public void CutComplete(int index) { controller.Cut(index); }
        public void ParryComplete(int index) { }
        
        /*void Cut()
        {
            controller.Cut();
            controller.Cut2();
        }*/
    }
}
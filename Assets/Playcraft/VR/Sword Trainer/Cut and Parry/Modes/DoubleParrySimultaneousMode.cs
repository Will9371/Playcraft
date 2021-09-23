namespace Playcraft.Examples.SwordTrainer
{
    public class DoubleParrySimultaneousMode : ISwordMode
    {
        SwordTrainer controller;
        public DoubleParrySimultaneousMode(SwordTrainer controller) { this.controller = controller; }

        public void Enter() { Parry(); }
        public void Exit() { }
        public void CutComplete() { }
        public void ParryComplete() { Parry(); }
        
        void Parry()
        {
            controller.Parry();
            controller.Parry2();
        }
    }
}
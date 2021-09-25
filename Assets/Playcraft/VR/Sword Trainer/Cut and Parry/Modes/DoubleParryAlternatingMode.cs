namespace Playcraft.Examples.SwordTrainer
{
    public class DoubleParryAlternatingMode : ISwordMode
    {
        SwordTrainer controller;
        public DoubleParryAlternatingMode(SwordTrainer controller) { this.controller = controller; }

        public void Enter() { controller.Parry(0); }
        //{ controller.RemoteRoutine(Process); }
        public void Exit() { }
        //{ controller.StopAllCoroutines(); }
        public void CutComplete(int index) { }
        public void ParryComplete(int index) { controller.CycleParry(index); }
        
        /*IEnumerator Process()
        {
            var delay = new WaitForSeconds(controller.halfActionTime);
            
            while (true)
            {
                yield return delay;
                controller.Parry(0);
                yield return delay;
                controller.Parry(1);
            }
        }*/
    }
}
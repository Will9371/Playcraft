namespace Playcraft.Examples.SwordTrainer
{
    public class DoubleCutAlternatingMode : ISwordMode
    {
        SwordTrainer controller;
        public DoubleCutAlternatingMode(SwordTrainer controller) { this.controller = controller; }

        public void Enter() { controller.Cut(0); }
        //{ controller.RemoteRoutine(Process); }
        public void Exit() { }
        //{ controller.StopAllCoroutines(); }
        public void CutComplete(int index) { controller.CycleCut(index); }
        public void ParryComplete(int index) { }
        
        /*IEnumerator Process()
        {
            var delay = new WaitForSeconds(controller.halfActionTime);
            
            while (true)
            {
                yield return delay;
                controller.Cut(0);
                yield return delay;
                controller.Cut(1);
            }
        }*/
    }
}
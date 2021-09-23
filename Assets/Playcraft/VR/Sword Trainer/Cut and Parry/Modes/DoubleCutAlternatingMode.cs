using UnityEngine;
using System.Collections;

namespace Playcraft.Examples.SwordTrainer
{
    public class DoubleCutAlternatingMode : ISwordMode
    {
        MonoSim mono => MonoSim.instance;

        SwordTrainer controller;
        public DoubleCutAlternatingMode(SwordTrainer controller) { this.controller = controller; }

        public void Enter() { mono.SimRoutine(Process); }
        
        // WARNING: will conflict with other uses of MonoSim
        public void Exit() { mono.StopAllCoroutines(); }

        public void CutComplete() { }
        public void ParryComplete() { }
        
        IEnumerator Process()
        {
            var delay = new WaitForSeconds(controller.halfActionTime);
            
            while (true)
            {
                yield return delay;
                controller.Cut();
                yield return delay;
                controller.Cut2();
            }
        }
    }
}
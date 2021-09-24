using UnityEngine;
using System.Collections;

namespace Playcraft.Examples.SwordTrainer
{
    public class DoubleParryAlternatingMode : ISwordMode
    {
        SwordTrainer controller;
        public DoubleParryAlternatingMode(SwordTrainer controller) { this.controller = controller; }

        public void Enter() { controller.RemoteRoutine(Process); }
        public void Exit() { controller.StopAllCoroutines(); }
        public void CutComplete() { }
        public void ParryComplete() { }
        
        IEnumerator Process()
        {
            var delay = new WaitForSeconds(controller.halfActionTime);
            
            while (true)
            {
                yield return delay;
                controller.Parry();
                yield return delay;
                controller.Parry2();
            }
        }
    }
}
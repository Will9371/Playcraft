using System.Collections;
using UnityEngine;

namespace Playcraft.Examples.SwordTrainer
{
    public class CutAndParryAlternatingMode : ISwordMode
    {
        SwordTrainer controller;
        public CutAndParryAlternatingMode(SwordTrainer controller) { this.controller = controller; }
        
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
                controller.Cut();
            }
        }
    }
}
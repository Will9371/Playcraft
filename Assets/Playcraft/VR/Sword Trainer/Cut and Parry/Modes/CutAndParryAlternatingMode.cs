using System.Collections;
using UnityEngine;

// OBSOLETE: merged with CutAndParryMode (consider renaming for consistency)
namespace Playcraft.Examples.SwordTrainer
{
    public class CutAndParryAlternatingMode : ISwordMode
    {
        SwordTrainer controller;
        public CutAndParryAlternatingMode(SwordTrainer controller) { this.controller = controller; }
        
        public void Enter() { controller.RemoteRoutine(Process); }
        public void Exit() { controller.StopAllCoroutines(); }
        public void CutComplete(int index) { }
        public void ParryComplete(int index) { }
        
        IEnumerator Process()
        {
            var delay = new WaitForSeconds(controller.halfActionTime);
            
            while (true)
            {
                yield return delay;
                controller.Parry(0);
                yield return delay;
                controller.Cut(0);
            }
        }
    }
}
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Playcraft.Examples.SwordTrainer
{
    public class MoveOneForward : MonoBehaviour
    {
        [SerializeField] LerpMirror[] actors;
        
        public void AdvanceRandom()
        {
            var index = Random.Range(0, actors.Length);
            
            for (int i = 0; i < actors.Length; i++)
                actors[i].SetDestination(i != index);
        }
    }
    
    [Serializable]
    public class LerpMirror
    {
        [SerializeField] LerpPositionOverTime actor;
        [SerializeField] LerpPositionOverTime mirror;
        
        public void SetDestination(bool forward)
        {
            actor.MoveIfNewDirection(forward);
            mirror.MoveIfNewDirection(forward);
        }
    }
}

using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Playcraft
{
    public class MoveOneForward : MonoBehaviour
    {
        [SerializeField] float advanceRetreatDelta;
        [SerializeField] LerpMirror[] actors;
        
        public void AdvanceRandom() { StartCoroutine(AdvanceRoutine()); }
        
        IEnumerator AdvanceRoutine()
        {
            var index = Random.Range(0, actors.Length);
            
            if (advanceRetreatDelta > 0f)
            {
                AdvanceOne(index);
                yield return new WaitForSeconds(advanceRetreatDelta);
                RetreatAllExceptOne(index);
            }
            else
            {
                RetreatAllExceptOne(index); 
                yield return new WaitForSeconds(-advanceRetreatDelta);
                AdvanceOne(index);               
            }
        }
        
        void AdvanceOne(int index) { actors[index].SetDestination(false); }
        
        void RetreatAllExceptOne(int index)
        {
            for (int i = 0; i < actors.Length; i++)
            {
                if (index == i) continue;
                actors[i].SetDestination(true);
            }
        }
    }
    
    [Serializable]
    public class LerpMirror
    {
        [SerializeField] LerpPositionOverTime actor;
        [SerializeField] LerpPositionOverTime mirror;
        [SerializeField] SplitBool mirrorColor;
        
        public void SetDestination(bool forward)
        {
            actor.MoveIfNewDirection(forward);
            mirror.MoveIfNewDirection(forward);
            mirrorColor.Input(forward);
        }
    }
}

using System.Collections;
using UnityEngine;

namespace Playcraft.Examples.SwordTrainer
{
    public class DodgePattern : MonoBehaviour
    {
        [SerializeField] int baseIndex;
        [SerializeField] int[] movementIndicies;
        [SerializeField] Vector2 baseWaitRange;
        [SerializeField] Vector2 moveWaitRange;
        [SerializeField] GetPercentOverTimeMono transition;
        [SerializeField] IntEvent output;

        void Start()
        {
            StartCoroutine(DodgeRoutine());
        }
        
        IEnumerator DodgeRoutine()
        {
            var transitionWait = new WaitForSeconds(transition.GetDuration());
        
            while (true)
            {
                yield return RandomStatics.RandomWait(baseWaitRange);
                output.Invoke(movementIndicies[Random.Range(0, movementIndicies.Length)]);
                transition.Begin();
                yield return transitionWait;
                yield return RandomStatics.RandomWait(moveWaitRange);
                output.Invoke(baseIndex);
                transition.Begin();
                yield return transitionWait;
            }
        }
    }
}

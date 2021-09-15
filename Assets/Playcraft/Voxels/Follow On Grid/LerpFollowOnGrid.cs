using System.Collections;
using UnityEngine;

namespace Playcraft.Examples.GridFollow
{
    public class LerpFollowOnGrid : MonoBehaviour
    {
        [SerializeField] LerpRotationIndexOverTime rotation;
        [SerializeField] float stoppingDistance = 1f;
        [SerializeField] float gridSize = 1f;   // * Replace with actually moving to center point
        [SerializeField] GetTargetDirection navigation;
        [SerializeField] FollowRoutine follow;
        [SerializeField] AttackRoutine attack;
        
        bool inStoppingDistance => navigation.targetDistance <= stoppingDistance;
        
        void Awake()
        {
            follow.Initialize(rotation, navigation, gridSize);
            attack.Initialize(rotation, navigation, gridSize);
        }

        public void Begin() { StartCoroutine(Chase()); }
        public void Stop() { StopAllCoroutines(); }

        IEnumerator Chase()
        {
            while (true)
            {
                if (inStoppingDistance)
                    yield return attack.Action();
                else
                    yield return follow.Action();
            }
        }
    }
}

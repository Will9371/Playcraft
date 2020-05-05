using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FindClosestTarget : MonoBehaviour
{    
    #pragma warning disable 0649
    [SerializeField] TransformEvent OnSetTarget;
    [SerializeField] UnityEvent OnClearTarget;
    #pragma warning restore 0649
    
    public void FindClosest(List<Transform> targets)
    {    
        if (targets.Count == 0)
            OnClearTarget.Invoke();
        else
        {
            var target = TransformMath.GetClosest(targets, transform.position);
            OnSetTarget.Invoke(target);
        }
    }
}

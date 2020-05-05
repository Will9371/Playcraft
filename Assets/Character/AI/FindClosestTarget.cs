using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FindClosestTarget : MonoBehaviour
{    
    [SerializeField] TransformEvent OnSetTarget;
    [SerializeField] UnityEvent OnClearTarget;
    
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

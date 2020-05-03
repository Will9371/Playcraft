using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FindClosestTarget : MonoBehaviour
{    
    [SerializeField] TransformEvent OnSetTarget;
    [SerializeField] UnityEvent OnSetNoTarget;
    
    public void FindClosest(List<Transform> targets)
    {    
        if (targets.Count == 0)
            OnSetNoTarget.Invoke();
        else
        {
            var target = TransformMath.GetClosest(targets, transform.position);
            OnSetTarget.Invoke(target);
        }
    }
}

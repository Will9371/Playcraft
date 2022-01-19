using System;
using UnityEngine;
using Playcraft;

[Serializable]
public class GoalieAI
{
    [Header("References")]
    public Transform self;
    public Transform target;
    public Transform center;
    
    [Header("Settings")]
    public float radius = 40f;
    public float speed = 10f;
    
    [SerializeField] bool stayInCircle = true;
    FollowOnCircle bounds = new FollowOnCircle();

    // * Consider delegating to SO or applying an abstraction layer  
    [SerializeField] bool usePrediction;
    [SerializeField] AverageVelocity prediction;
    Vector3 targetPosition => usePrediction ? prediction.projectedPosition : target.position;
    
    public void OnValidate()
    {
        bounds.radius = radius;
    }
    
    public void Update()
    {
        if (stayInCircle)
        {
            bounds.center = center.position;
            self.position = Vector3.MoveTowards(self.position, bounds.Update(targetPosition), speed * Time.deltaTime);
        }
        else
            self.position = Vector3.MoveTowards(self.position, targetPosition, speed * Time.deltaTime);
    }
    
    public void FixedUpdate() { if (usePrediction) prediction.FixedUpdate(target.position); }
}

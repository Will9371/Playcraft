using UnityEngine;

namespace Playcraft
{
    public class CircleTargetAtDistance : MonoBehaviour
    {
        [SerializeField] [Range(0, 1)] float circleMinWeight = 1f;
        [SerializeField] [Range(0, 1)] float circleMaxWeight = 1f;

        [SerializeField] [Range(0, 1)] float distanceMinWeight = 1f;
        [SerializeField] [Range(0, 1)] float distanceMaxWeight = 1f;
        
        [SerializeField] Transform self;
        [SerializeField] CircleTarget circleTarget;
        [SerializeField] MaintainDistance maintainDistance;
        [SerializeField] DriftTranslateStep movement;
        
        public bool circlingEnabled = true;
        public bool distancingEnabled = true;
        
        float circleWeight = 1f;
        float distanceWeight = 1f;

        void OnValidate() { SetSelf(); }
        
        void OnEnable()
        {
            RandomizeWeights();
            circleTarget.SetRandomDirection();
        }

        void SetSelf()
        {
            if (!self) self = transform;

            circleTarget.self = self;
            maintainDistance.self = self;
            movement.self = self;
        }
        
        public void SetTarget(Transform value) 
        { 
            circleTarget.target = value;
            maintainDistance.target = value; 
        }
        
        public void RandomizeWeights()
        {
            circleWeight = Random.Range(circleMinWeight, circleMaxWeight);
            distanceWeight = Random.Range(distanceMinWeight, distanceMaxWeight);
        }
        
        Vector3 moveStep;

        void Update() 
        {
             if (circlingEnabled)
             {
                moveStep = circleTarget.Update() * circleWeight;
                movement.AddMovement(moveStep);
             }
             
             if (distancingEnabled)
             {
                moveStep = maintainDistance.Update() * distanceWeight;
                movement.AddMovement(moveStep);
             }
             
             movement.Update();
        }
        
        public void SetDesiredDistance(float value) { maintainDistance.SetDesiredDistance(value); }
    }
}
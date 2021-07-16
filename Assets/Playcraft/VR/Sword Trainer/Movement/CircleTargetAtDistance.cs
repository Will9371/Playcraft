using UnityEngine;

namespace Playcraft.Examples.SwordTrainer
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
        
        [HideInInspector] public bool circlingEnabled = true;
        [HideInInspector] public bool distancingEnabled = true;
        
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

        void Update() 
        { 
             if (circlingEnabled)
                movement.AddMovement(circleTarget.Update() * circleWeight);
             
             if (distancingEnabled)
                movement.AddMovement(maintainDistance.Update() * distanceWeight);
             
             movement.Update();
        }
        
        public void SetDesiredDistance(float value) { maintainDistance.SetDesiredDistance(value); }
    }
}
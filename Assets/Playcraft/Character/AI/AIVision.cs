using UnityEngine;

namespace Playcraft.AI
{
    public class AIVision : MonoBehaviour
    {
        [Tooltip("Time in seconds between each vision refresh.  Simulates reaction time.")]
        [SerializeField] float refreshRate = 0.5f;
        [SerializeField] bool applyAngleFilter = true;
        [SerializeField] bool applyLineOfSightFilter = true;
        [SerializeField] ColliderEvent ClosestTarget;
        
        TrackColliders trackColliders = new TrackColliders();
        [SerializeField] FilterByAngle filterByAngle;
        [SerializeField] LineOfSight lineOfSight;
        [SerializeField] FilterCollidersByCustomTag targetFilter;
        
        
        void OnEnable() { InvokeRepeating(nameof(Refresh), refreshRate, refreshRate); }
        void OnDisable() { CancelInvoke(nameof(Refresh)); }

        void OnTriggerEnter(Collider other) { trackColliders.OnTriggerEnter(other); }
        void OnTriggerExit(Collider other) { trackColliders.OnTriggerExit(other); }
        
        // TBD: make some stages optional
        void Refresh() 
        { 
            var targets = trackColliders.Refresh();
            if (applyAngleFilter) targets = filterByAngle.Input(targets);
            if (applyLineOfSightFilter) targets = lineOfSight.Input(targets);
            targets = targetFilter.Input(targets);
            var target = VectorMath.GetClosest(targets, transform.position);
            ClosestTarget.Invoke(target);
            //Debug.Log($"Nearby: {nearby.Count}, in view: {inView.Count}, in sight: {inSight.Count}, " +
            //          $"targets: {visibleTargets.Count}, closest: {closestTarget}");
        }
        
        void OnValidate() { filterByAngle.Validate(); }
        public void SetMaxAngle(float value) { filterByAngle.SetMaxAngle(value); }
    }
}

//[SerializeField] FindClosestCollider findClosest;
//var target = findClosest.Input(targets);
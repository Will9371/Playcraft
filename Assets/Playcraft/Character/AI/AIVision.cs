using UnityEngine;

namespace Playcraft.AI
{
    public class AIVision : MonoBehaviour
    {
        [SerializeField] Transform source;
        [SerializeField] float refreshRate = 0.5f;
        [Tooltip("True: only detect colliders with TransformChildContainer component attached.  " +
                 "False: raycast to center of colliders without TransformChildContainer.")]
        [SerializeField] bool requireMultiplePoints;
        [Tooltip("True: draw debug lines for all raycasts.")]
        [SerializeField] [Range(0f, 360f)] float maxAngle = 45f;
        [SerializeField] SO[] targetIds;
        [SerializeField] ColliderEvent ClosestTarget;
        [SerializeField] bool debug;
    
        #region Internal Dependencies
        
        TrackColliders trackColliders = new TrackColliders();
        FilterByAngle _filterByAngle;
        FilterByAngle filterByAngle
        {
            get
            {
                if (_filterByAngle == null)
                    _filterByAngle = new FilterByAngle(source, maxAngle);
                    
                return _filterByAngle;
            }
        }
        LineOfSight lineOfSight;
        FilterCollidersByCustomTag targetFilter;
        FindClosestCollider findClosest;                  
        
        void Awake()
        {
            lineOfSight = new LineOfSight(source, requireMultiplePoints, debug);
            targetFilter = new FilterCollidersByCustomTag(targetIds);
            findClosest = new FindClosestCollider(source);
        }
        
        #endregion
        
        void OnEnable() { InvokeRepeating(nameof(Refresh), refreshRate, refreshRate); }
        void OnDisable() { CancelInvoke(nameof(Refresh)); }

        void OnTriggerEnter(Collider other) { trackColliders.OnTriggerEnter(other); }
        void OnTriggerExit(Collider other) { trackColliders.OnTriggerExit(other); }
        
        void Refresh() 
        { 
            var nearby = trackColliders.Refresh();
            var inView = filterByAngle.Input(nearby);
            var inSight = lineOfSight.Input(inView);
            var visibleTargets = targetFilter.Input(inSight);
            var closestTarget = findClosest.Input(visibleTargets);
            ClosestTarget.Invoke(closestTarget);           
        }
        
        void OnValidate() { SetMaxAngle(maxAngle); }
        
        public void SetMaxAngle(float value)
        {
            maxAngle = value;
            filterByAngle.SetMaxAngle(maxAngle);           
        }
    }
}

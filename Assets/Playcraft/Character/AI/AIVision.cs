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
        
        Track_Colliders trackColliders = new Track_Colliders();
        Filter_By_Angle _filterByAngle;
        Filter_By_Angle filterByAngle
        {
            get
            {
                if (_filterByAngle == null)
                    _filterByAngle = new Filter_By_Angle(source, maxAngle);
                    
                return _filterByAngle;
            }
        }
        Line_Of_Sight lineOfSight;
        FilterCollidersByCustomTag targetFilter;
        Find_Closest_Collider findClosest;                  
        
        void Awake()
        {
            lineOfSight = new Line_Of_Sight(source, requireMultiplePoints, debug);
            targetFilter = new FilterCollidersByCustomTag(targetIds);
            findClosest = new Find_Closest_Collider(source);
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

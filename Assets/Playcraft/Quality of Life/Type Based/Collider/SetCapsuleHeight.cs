using UnityEngine;

namespace Playcraft
{
    public class SetCapsuleHeight : MonoBehaviour
    {
        [SerializeField] CapsuleCollider capsule;
        [SerializeField] float changeSpeed = 1f;
        
        float targetHeight;
        
        void Start()
        {
            targetHeight = capsule.height;
        }
        
        public void InputTargetHeight(float value) { targetHeight = value; }

        void Update()
        {
            var heightDelta = targetHeight - capsule.height;
            if (Mathf.Abs(heightDelta) < .01f)
                return;
                
            var scaleDirection = heightDelta < 0 ? -1f : 1f;
            var scaleStep = changeSpeed * targetHeight * scaleDirection * Time.deltaTime;
            capsule.height += scaleStep;
        }
    }
}
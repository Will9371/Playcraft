using System;
using UnityEngine;
using Playcraft.Pooling;

namespace Playcraft.Examples.SwordTrainer
{
    [Serializable]
    public class CreateFloatingNumber
    {
        [SerializeField] Transform center;
        [SerializeField] GameObject floaterPrefab;
        [SerializeField] Transform canvas;
        [SerializeField] Transform faceTarget;
        [SerializeField] RectTransform template;
            
        ObjectPoolMaster spawner => ObjectPoolMaster.instance;
            
        public void SetFloaterCanvas(Transform value) { canvas = value; }
        public void SetFloaterFaceTarget(Transform value) { faceTarget = value; }
            
        public void Generate(Vector3 incomingDirection, float number)
        {
            // Generate floating number
            var floaterObj = spawner.Spawn(floaterPrefab, center.position);
            floaterObj.transform.SetParent(canvas);
            floaterObj.SetActive(true);
                
            // Set floater's rotation and scale
            var rect = floaterObj.transform as RectTransform;
            rect.localRotation = template.localRotation;
            rect.localScale = template.localScale;
                
            // Set floater in motion
            var floater = floaterObj.GetComponent<FloatingNumber>();      
            floater.Begin(center, incomingDirection, number);
                
            // (Optional) face the player
            if (faceTarget)
            {
                var facing = floaterObj.GetComponent<FaceTargetInstant>();
                facing.SetTarget(faceTarget);
            }
        }
    }
}

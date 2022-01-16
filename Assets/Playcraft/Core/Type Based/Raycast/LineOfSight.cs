using System;
using System.Collections.Generic;
using UnityEngine; 
    
namespace Playcraft
{
    [Serializable]
    public class LineOfSight
    {
        [SerializeField] Transform source;
        [Tooltip("True: only detect colliders with TransformChildContainer component attached.  " +
                 "False: raycast to center of colliders without TransformChildContainer.")]
        [SerializeField] bool requireMultiplePoints;
        [Tooltip("True: draw debug lines for all raycasts.")]
        [SerializeField] bool debug;

        List<Collider> inSight = new List<Collider>();
        
        Vector3 targetVector;
        Ray ray;
        RaycastHit hit;
        bool isHit;
        bool anyLineIsClear;

        public List<Collider> Input(List<Collider> values)
        {
            inSight.Clear();
        
            foreach (var value in values)
            {
                var points = value.GetComponent<TransformChildContainer>();
                
                if (!points) 
                {
                    if (!requireMultiplePoints && LineIsClear(value))
                        inSight.Add(value);
                        
                    continue;
                }
            
                anyLineIsClear = false;
            
                foreach (var point in points.points)
                    if (LineIsClear(point.position, value))
                        anyLineIsClear = true;
                
                if (anyLineIsClear) inSight.Add(value);
            }
            
            return inSight;            
        }
        
        bool LineIsClear(Collider other) { return LineIsClear(other.transform.position, other); }  
        bool LineIsClear(Vector3 position, Collider other)
        {
            targetVector = position - source.position;
            ray = new Ray(source.position, targetVector);
            isHit = Physics.Raycast(ray, out RaycastHit hit);
            if (!isHit) return false;
                    
            if (debug) 
            {
                var color = hit.collider == other ? Color.green : Color.red;
                Debug.DrawLine(source.position, position, color, .5f);
            }
            
            return hit.collider == other;  
        }
    } 
}
using System;
using UnityEngine;

namespace Playcraft
{
    public class TransformChildContainer : MonoBehaviour
    {
        [SerializeField] Transform container;
         
        [NonSerialized] public Transform[] points;
        
        void Awake()
        {
            points = new Transform[container.childCount];
            
            for (int i = 0; i < points.Length; i++)
                points[i] = container.GetChild(i);
        }
    }
}

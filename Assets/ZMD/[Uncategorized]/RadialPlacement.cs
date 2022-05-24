using System;
using UnityEngine;

[Serializable]
public class RadialPlacement
{
    [SerializeField] Transform centerPoint;
    [SerializeField] [Range(0, 360)] float range = 360;
    [SerializeField] float distance = 1f;
    
    public void Place(Transform[] placedPrefabs)
    {
        var count = placedPrefabs.Length;
        var increment = range/count;
        
        if (range + increment < 360 && count > 1)
            increment += increment/(count - 1);
        
        for (int i = 0; i < count; i++)
        {
            var angle = increment * i - range/2;
            placedPrefabs[i].localEulerAngles = centerPoint.eulerAngles + new Vector3(0, angle, 0);
            placedPrefabs[i].localPosition = centerPoint.position + placedPrefabs[i].forward * distance;
        }
    }
}

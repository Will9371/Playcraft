using System.Collections.Generic;
using UnityEngine;

// RENAME, wrap in experimental namespace
public class DrawLineBetweenNodes : MonoBehaviour
{
    [SerializeField] Vector3ArrayEvent Positions;
    
    List<Transform> nodes = new List<Transform>();

    public void RequestAdd(Transform value)
    {
        // Exit early if the last node in the list matches the newly added node
        if (nodes.Count > 0 && nodes[nodes.Count - 1] == value) return;
        
        nodes.Add(value);
        Refresh();
    }
    
    void Refresh()
    {
        var points = new Vector3[nodes.Count];
        
        for (int i = 0; i < nodes.Count; i++)
            points[i] = nodes[i].position;
        
        Positions.Invoke(points);
    }
    
    public void Clear()
    {
        nodes.Clear();
        Refresh();
    }
}

using UnityEngine;

// RENAME: CycleVector3? (there is a similar class that works with another data type somewhere...)
public class PositionHistory
{
    Vector3[] history;
    
    int index;
    Vector3 current;
    
    public void Initialize(Vector3 startPosition, int recordCount)
    {
        index = 0;
        history = new Vector3[recordCount];
        
        for (int i = 0; i < recordCount; i++)
            history[i] = startPosition;
    }

    public Vector3 FixedUpdate(Vector3 newPosition)
    {
        current = history[index];
        history[index] = newPosition;
        
        index++;
        if (index >= history.Length)
            index = 0;
        
        return current;
    }
}

using Playcraft;
using UnityEngine;

public class Lerp_Position
{
    readonly Transform self;
    readonly bool useLocal;
    Vector3[] positions;

    public Vector3 start;
    public Vector3 end;  
    int index;
    
    public Lerp_Position(Transform self, bool useLocal)
    {
        this.self = self;
        this.useLocal = useLocal;
    }     
        
    public Lerp_Position(Transform self, Vector3[] positions, int defaultIndex = 0, bool useLocal = true)
    {
        this.self = self;
        this.positions = positions;
        this.useLocal = useLocal;
            
        if (positions.Length <= 0) return;

        index = defaultIndex;
        start = positions[defaultIndex];
        Input(0f);
    }

    // Move between internally-stored positions
    public void SetDestination(int newIndex)
    {
        if (newIndex >= positions.Length)
        {
            Debug.LogError("Attempting to set destination index " + 
                            newIndex + " of " + positions.Length);
            return;
        }

        start = positions[index];
        end = positions[newIndex];
        index = newIndex;
    }

    // Move towards externally defined location
    public void SetDestination(Vector3 destination)
    {
        start = self.position;
        end = destination;
    }

    Vector3 position;

    // Call continuously to move over time
    public void Input(float percent)
    {
        position = Vector3.Lerp(start, end, percent);
        if (useLocal) self.localPosition = position;
        else self.position = position;
    }

    public void SetDestinations(Vector3Array input) { SetDestinations(input.values); }
    public void SetDestinations(Vector3[] input)
    {
        positions = new Vector3[input.Length];
        
        for (int i = 0; i < input.Length; i++)
            positions[i] = input[i];            
    }
}

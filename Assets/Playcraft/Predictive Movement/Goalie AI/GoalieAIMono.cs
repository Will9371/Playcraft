using UnityEngine;

public class GoalieAIMono : MonoBehaviour
{
    [SerializeField] GoalieAI process;
    
    void OnValidate() 
    {
        process.self = transform; 
        process.OnValidate(); 
    }
    
    void Update() { process.Update(); }
    void FixedUpdate() { process.FixedUpdate(); }
}

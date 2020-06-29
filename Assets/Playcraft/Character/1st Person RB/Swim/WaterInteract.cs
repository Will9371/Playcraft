using UnityEngine;
using UnityEngine.Events;

public class WaterInteract : MonoBehaviour
{
    [SerializeField] UnityEvent EnterWater;
    [SerializeField] UnityEvent ExitWater;

    public void RequestEnter(Collider other)
    {
        if (IsWater(other)) 
            EnterWater.Invoke();
    }
    
    public void RequestExit(Collider other)
    {
        if (IsWater(other)) 
            ExitWater.Invoke();        
    }
    
    private bool IsWater(Collider other)
    {
        return other.GetComponent<WaterTag>();
    }
}

using UnityEngine;

public class TestReceiver : MonoBehaviour
{
    public void Print(string message)
    {
        Debug.Log(message);
    }
    
    public void Print(Vector2 input)
    {
        Debug.Log(input);
    }
}

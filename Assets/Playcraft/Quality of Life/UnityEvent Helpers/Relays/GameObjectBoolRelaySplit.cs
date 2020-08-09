using UnityEngine;

public class GameObjectBoolRelaySplit : MonoBehaviour
{
    [SerializeField] GameObjectEvent True = default;
    [SerializeField] GameObjectEvent False = default;
    
    public void Input(GameObject source, bool active) 
    {
        if (active) True.Invoke(source); 
        else False.Invoke(source);
    }
}

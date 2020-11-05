using UnityEngine;

public class SetObjectsActive : MonoBehaviour
{
    [SerializeField] GameObject[] objects;
    
    public void SetAllActive(bool value)
    {
        foreach (var obj in objects)
            obj.SetActive(value);
    }
}

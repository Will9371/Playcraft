using UnityEngine;
using UnityEngine.Events;

public class UpdateEvent : MonoBehaviour
{
    [SerializeField] UnityEvent OnUpdate;
    void Update() { OnUpdate.Invoke(); }
}

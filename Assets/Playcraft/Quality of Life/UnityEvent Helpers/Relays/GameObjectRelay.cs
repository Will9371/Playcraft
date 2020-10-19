using UnityEngine;

public class GameObjectRelay : MonoBehaviour, ISetObject
{
    [SerializeField] GameObjectEvent Output = default;
    public void SetObject(GameObject value) { Output.Invoke(value); }
}

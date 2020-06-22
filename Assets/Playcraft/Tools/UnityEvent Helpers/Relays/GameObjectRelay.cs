using UnityEngine;

public class GameObjectRelay : MonoBehaviour
{
    [SerializeField] GameObjectEvent Output = default;
    public void Input(GameObject value) { Output.Invoke(value); }
}

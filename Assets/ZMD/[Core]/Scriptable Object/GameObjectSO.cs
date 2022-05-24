using UnityEngine;

namespace ZMD
{
    [CreateAssetMenu(menuName = "ZMD/Data Types/GameObject", fileName = "Game Object Reference")]
    public class GameObjectSO : ScriptableObject { public GameObject value; }
}
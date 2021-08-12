using UnityEngine;

namespace Playcraft
{
    [CreateAssetMenu(menuName = "Playcraft/Data Types/GameObject", fileName = "Game Object Reference")]
    public class GameObjectSO : ScriptableObject { public GameObject value; }
}
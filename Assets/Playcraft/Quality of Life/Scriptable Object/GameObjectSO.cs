using System;
using UnityEngine;

namespace Playcraft
{
    [CreateAssetMenu(menuName = "Playcraft/Data Types/GameObject", fileName = "Game Object Reference")]
    public class GameObjectSO : ScriptableObject { [NonSerialized] public GameObject value; }
}
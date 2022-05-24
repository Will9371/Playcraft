using UnityEngine;

namespace ZMD.Scene
{
    [CreateAssetMenu(menuName = "ZMD/Scene Management/Transition")]
    public class SceneTransitionSO : ScriptableObject
    {
        public float minimumLoadScreenTime = 2f;
        public StringSO[] scenesToLoad;
        public StringSO[] scenesToUnload;
    }
}
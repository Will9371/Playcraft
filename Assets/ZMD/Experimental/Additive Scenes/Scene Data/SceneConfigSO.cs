using System;
using UnityEngine;

namespace ZMD.Scene
{
    [CreateAssetMenu(menuName = "ZMD/Scene Management/Scene Config List")]
    public class SceneConfigSO : ScriptableObject
    {
        public StringSO[] universalScenes;
        public SceneConfig[] sceneConfigurations;
    }

    [Serializable] public class SceneConfig
    {
        public bool isDefaultStartingScene;
        public StringSO uniqueScene;
        public StringSO[] supportingScenes;
    }
}

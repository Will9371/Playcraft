using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Playcraft
{
    public class RespondToLoadedScene : MonoBehaviour
    {
        [SerializeField] Binding[] bindings = default;

        void OnEnable() { SceneManager.sceneLoaded += OnSceneLoaded; }
        void OnDisable() { SceneManager.sceneLoaded -= OnSceneLoaded; }
        
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            foreach (var binding in bindings)
                if (binding.scene.value == scene.name)
                    binding.Response.Invoke();
        }
        
        [Serializable] public struct Binding
        {
            public StringSO scene;
            public UnityEvent Response;
        }
    }
}

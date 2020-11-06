using System;
using Playcraft;
using UnityEngine;
using UnityEngine.SceneManagement;

// NOT USED
public class SceneStateTracker : Singleton<SceneStateTracker>
{
    #pragma warning disable 0649
    [SerializeField] SceneSO[] scenes;
    [SerializeField] SceneState[] states;
    #pragma warning restore 0649

    void Awake()
    {
        states = new SceneState[scenes.Length];
        
        for (int i = 0; i < scenes.Length; i++)
            states[i] = new SceneState(scenes[i].sceneName);
    }
    
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }
    
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var state = GetSceneState(scene.name);
        state.active = true;  
    }
    
    void OnSceneUnloaded(Scene scene)
    {
        var state = GetSceneState(scene.name);
        state.active = false;        
    }
    
    public bool GetSceneInitialized(string scene)
    {
        var state = GetSceneState(scene);
        return state.initialized;
    }
    
    public void SetSceneInitialized(string scene, bool value)
    {
        Debug.Log(scene + " " + value);
        var state = GetSceneState(scene);
        state.initialized = value;
    }
    
    SceneState GetSceneState(string scene)
    {
        foreach (var state in states)
            if (state.name == scene)
                return state;
                
        return null;
    }
        
    [Serializable] public class SceneState
    {
        public string name;
        public bool initialized;
        public bool active;
        
        public SceneState(string name)
        {
            this.name = name;
            initialized = false;
            active = false;
        }
    }
}

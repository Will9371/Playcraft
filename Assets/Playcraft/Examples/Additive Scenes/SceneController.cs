using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Playcraft;
using UnityEngine;

public class SceneController : Singleton<SceneController>
{
    [SerializeField] SceneConfigSO sceneConfigData = default;
    
    List<string> activeScenes = new List<string>();
    
    public void RefreshActiveScenes()
    {
        activeScenes.Clear();
        for (int i = 0; i < SceneManager.sceneCount; i++)
            activeScenes.Add(SceneManager.GetSceneAt(i).name);
    }
    
    void LoadIfNotPresent(SceneSO value)
    {
        LoadIfNotPresent(value.sceneName.ToString());
    }
    
    void LoadIfNotPresent(string value)
    {
        if (activeScenes.Contains(value)) return;
        SceneManager.LoadScene(value, LoadSceneMode.Additive);
        activeScenes.Add(value);        
    }
    
    void UnloadIfPresent(SceneSO value)
    {
        UnloadIfPresent(value.sceneName.ToString()); 
    }
    
    void UnloadIfPresent(string value)
    {
        if (!activeScenes.Contains(value)) return;
        SceneManager.UnloadSceneAsync(value);
        activeScenes.Remove(value);        
    }
    
    public void LoadUnload(SceneTransitionSO transition)
    {
        LoadUnload(transition.scenesToLoad, transition.scenesToUnload);
    }
    
    public void LoadUnload(SceneSO[] scenesToLoad, SceneSO[] scenesToUnload)
    {
        RefreshActiveScenes();
        
        foreach (var scene in scenesToLoad)
            LoadIfNotPresent(scene);
        foreach (var scene in scenesToUnload)
            UnloadIfPresent(scene);            
    }
    
    public void ValidateCurrentScenes()
    {
        RefreshActiveScenes();
            
        // Determine which group of scenes to load based on what is currently loaded
        SceneConfig uniqueSceneFound = null;
        SceneConfig defaultSceneFound = null;
        
        foreach (var config in sceneConfigData.sceneConfigurations)
        {
            if (activeScenes.Contains(config.uniqueScene.sceneName.ToString()))
            {
                uniqueSceneFound = config;
                break;
            }
            if (config.isDefaultStartingScene)
                defaultSceneFound = config;
        }
        
        var configToLoad = uniqueSceneFound == null ? defaultSceneFound : uniqueSceneFound;
        
        var sceneNamesToLoad = new List<string>();
        
        foreach (var scene in sceneConfigData.universalScenes)
            sceneNamesToLoad.Add(scene.sceneName.ToString());
        
        sceneNamesToLoad.Add(configToLoad.uniqueScene.sceneName.ToString());
        
        foreach (var scene in configToLoad.supportingScenes)
            sceneNamesToLoad.Add(scene.sceneName.ToString());
            
        foreach (var scene in sceneNamesToLoad)
            LoadIfNotPresent(scene);
            
        foreach (var scene in activeScenes)
            if (!sceneNamesToLoad.Contains(scene))
                UnloadIfPresent(scene);
    }
}

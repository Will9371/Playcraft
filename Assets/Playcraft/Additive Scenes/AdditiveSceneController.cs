using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace Playcraft
{
    public class AdditiveSceneController : Singleton<AdditiveSceneController>
    {
        [SerializeField] SceneConfigSO sceneConfigData = default;
        
        [Tooltip("Exposed for inspector visibility and read access, do not modify externally")]
        public List<string> activeScenes = new List<string>();
        
        void LoadIfNotPresent(StringSO value) { LoadIfNotPresent(value.value); }
        void LoadIfNotPresent(string value)
        {
            if (activeScenes.Contains(value)) return;
            SceneManager.LoadScene(value, LoadSceneMode.Additive);
            activeScenes.Add(value);
        }
        
        void UnloadIfPresent(StringSO value) { UnloadIfPresent(value.value); }
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
        
        void LoadUnload(StringSO[] scenesToLoad, StringSO[] scenesToUnload)
        {    
            RefreshActiveScenes();
            
            foreach (var scene in scenesToLoad)
                LoadIfNotPresent(scene);
            foreach (var scene in scenesToUnload)
                UnloadIfPresent(scene);
        }
        
        void RefreshActiveScenes()
        {
            activeScenes.Clear();
            for (int i = 0; i < SceneManager.sceneCount; i++)
                activeScenes.Add(SceneManager.GetSceneAt(i).name);
        }
        
        public void ValidateCurrentScenes()
        {
            RefreshActiveScenes();
            
            // Get scene list to load
            var config = GetSceneConfig();
            var sceneNames = GetSceneNames(config);
                
            // Load new scenes
            foreach (var scene in sceneNames)
                LoadIfNotPresent(scene);
                
            // Unload old scenes
            foreach (var scene in activeScenes)
                if (!sceneNames.Contains(scene))
                    UnloadIfPresent(scene);                
        }
        
        SceneConfig GetSceneConfig()
        {
            SceneConfig uniqueSceneFound = null;
            SceneConfig defaultSceneFound = null;
            
            foreach (var config in sceneConfigData.sceneConfigurations)
            {
                if (activeScenes.Contains(config.uniqueScene.value))
                {
                    uniqueSceneFound = config;
                    break;
                }
                if (config.isDefaultStartingScene)
                    defaultSceneFound = config;
            }
            
            return uniqueSceneFound == null ? defaultSceneFound : uniqueSceneFound;        
        }
        
        List<string> GetSceneNames(SceneConfig config)
        {
            var sceneNames = new List<string>();
            
            foreach (var scene in sceneConfigData.universalScenes)
                sceneNames.Add(scene.value);
            
            sceneNames.Add(config.uniqueScene.value);
            
            foreach (var scene in config.supportingScenes)
                sceneNames.Add(scene.value);

            return sceneNames;
        }
    }
}

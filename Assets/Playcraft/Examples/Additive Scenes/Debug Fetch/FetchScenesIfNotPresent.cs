using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Limited, non-singleton version of SceneController used to get the scene with SceneController
public class FetchScenesIfNotPresent : MonoBehaviour
{
    [SerializeField] SceneSO[] scenesToFetch = default;
    List<string> activeScenes = new List<string>();
    
    public void Fetch()
    {
        RefreshActiveScenes();
        
        foreach (var scene in scenesToFetch)
            LoadIfNotPresent(scene);
    }
    
    void RefreshActiveScenes()
    {
        activeScenes.Clear();
        for (int i = 0; i < SceneManager.sceneCount; i++)
            activeScenes.Add(SceneManager.GetSceneAt(i).name);
    }
    
    void LoadIfNotPresent(SceneSO value)
    {
        LoadIfNotPresent(value.sceneName);
    }
    
    void LoadIfNotPresent(string value)
    {
        if (activeScenes.Contains(value)) return;
        SceneManager.LoadScene(value, LoadSceneMode.Additive);
        activeScenes.Add(value);        
    }
}

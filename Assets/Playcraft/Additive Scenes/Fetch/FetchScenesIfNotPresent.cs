using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Playcraft
{
    public class FetchScenesIfNotPresent : MonoBehaviour
    {
        [SerializeField] SceneSO preload = default;
            
        List<string> activeScenes = new List<string>();
        
        void Start() 
        { 
            RefreshActiveScenes();
            LoadIfNotPresent(preload);
        }
        
        void RefreshActiveScenes()
        {
            activeScenes.Clear();
            for (int i = 0; i < SceneManager.sceneCount; i++)
                activeScenes.Add(SceneManager.GetSceneAt(i).name);
        }
        
        void LoadIfNotPresent(SceneSO value) { LoadIfNotPresent(value.sceneName); }
        void LoadIfNotPresent(string value)
        {
            if (activeScenes.Contains(value)) return;
            SceneManager.LoadScene(value, LoadSceneMode.Additive);
            activeScenes.Add(value);        
        }
        
        AdditiveSceneController controller => AdditiveSceneController.instance;
        public void ControllerValidate() { controller.ValidateCurrentScenes(); }
    }
}

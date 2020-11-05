using System.Collections;
using UnityEngine;
using UnityEngine.Events;

// NOT USED
public class CheckIfInitialized : MonoBehaviour
{
    [SerializeField] SceneSO self;
    [SerializeField] UnityEvent Initialized;
    [SerializeField] UnityEvent Uninitialized;

    SceneStateTracker tracker => SceneStateTracker.instance;
    
    void OnEnable()
    {
        StartCoroutine(WaitForTracker());
    }
    
    IEnumerator WaitForTracker()
    {
        while (!SceneStateTracker.exists)
        {
            // NG: waits endlessly
            //Debug.Log("Waiting for tracker...");
            yield return null;
        }
            
        CheckInitialization();
    }
    
    void CheckInitialization()
    {
        var initialized = tracker.GetSceneInitialized(self.sceneName);
        
        Debug.Log(initialized);
        if (initialized)
            Initialized.Invoke();
        else
        {
            Uninitialized.Invoke();
            tracker.SetSceneInitialized(self.sceneName, true);
        }        
    }
}

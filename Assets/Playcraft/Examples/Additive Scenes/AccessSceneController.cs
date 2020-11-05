using System.Collections;
using UnityEngine;

public class AccessSceneController : MonoBehaviour
{
    SceneController scene => SceneController.instance;
    
    public void LoadUnload(SceneTransitionSO transition)
    {
        scene.LoadUnload(transition);
    }
    
    public void ValidateCurrentScenes()
    {
        StartCoroutine(WaitToValidteCurrentScenes());
    }
    
    IEnumerator WaitToValidteCurrentScenes()
    {
        while (!SceneController.exists)
            yield return null;
    
        scene.ValidateCurrentScenes();        
    }
}

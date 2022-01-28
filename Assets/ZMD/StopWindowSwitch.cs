#if UNITY_EDITOR

using System.Collections;
using UnityEngine;
using UnityEditor;

/// Allows starting playmode in Editor
/// When pausing, will stick to whatever window was open last time setting window while paused
/// INCOMPLETE: want to stick to whatever window was open last, but the UnityEditor has a garbage interface...
/// OBSOLETE: Use twicebetter's KeepSceneFocused instead
public class StopWindowSwitch : MonoBehaviour
{
    [SerializeField] bool startInScene;
    [SerializeField] bool alwaysUnpauseToScene;
    
    bool focusExists => EditorWindow.focusedWindow;
    string focusString => focusExists ? EditorWindow.focusedWindow.ToString() : "";
    string sceneViewLabel => " (UnityEditor.SceneView)";
    bool inScene => focusString == sceneViewLabel;
    
    bool wasInScene => oldPath == "Window/General/Scene";
    string generalPath => "Window/General/";
    string menuPath => generalPath + (inScene ? "Scene" : "Game");
    string oldPath => priorPaths[priorPaths.Length - 1];
    
    const int frameWait = 2;    // Focus is null after 1st frame after unpause, GameView after 2nd
    string[] priorPaths = new string[100];
    
    void Awake() { EditorApplication.pauseStateChanged += PauseStateChanged; }
    void OnDestroy() { EditorApplication.pauseStateChanged -= PauseStateChanged; }
    
    void Start() { if (startInScene) SceneView.FocusWindowIfItsOpen(typeof(SceneView));}

    void Update()
    {
        priorPaths[0] = menuPath;
        for (int i = 0; i < priorPaths.Length - 1; i++)
            priorPaths[priorPaths.Length - (i + 1)] = priorPaths[priorPaths.Length - (i + 2)];
    }
    
    void PauseStateChanged(PauseState pauseState)
    {
        if (pauseState == PauseState.Unpaused)
            StartCoroutine(KeepPriorViewOnUnpause(alwaysUnpauseToScene));
        else if (wasInScene)
            SceneView.FocusWindowIfItsOpen(typeof(SceneView));

        // I don't know how to force GameView when paused
            // EditorApplication.ExecuteMenuItem(priorPaths[priorPaths.Length - 1]); // Only works in playmode
            //SceneView.FocusWindowIfItsOpen(typeof(GameView));  // Not allowed because GameView is internal
    }

    IEnumerator KeepPriorViewOnUnpause(bool forceScene)
    {
        for (int i = 0; i < frameWait; i++)
            yield return null;

        if (forceScene)
            SceneView.FocusWindowIfItsOpen(typeof(SceneView));
        else
            EditorApplication.ExecuteMenuItem(oldPath);
    }
}

#endif

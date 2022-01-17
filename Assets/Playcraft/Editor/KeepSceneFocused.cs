// CREDIT: n-gist
// SOURCE: https://forum.unity.com/threads/dont-switch-to-game-tab-when-i-press-play-button.159530/

#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace twicebetter 
{
    [InitializeOnLoad]
    internal static class KeepSceneFocused 
    {
        public static bool   forceFocusSceneOnPlay = true;
        public static bool   disabled = false;
        static SceneView     sceneWindow;
        static EditorWindow  gameWindow;
        static bool          sceneNeedFocus;
        static bool          oneFrameSkipped;
        static System.Action onFocus;
        
        static KeepSceneFocused() 
        {
            FindSceneWindow();
            FindGameWindow();
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }
        
        static void FindSceneWindow() 
        {
            if (sceneWindow != null) return;
            var sceneWindows = Resources.FindObjectsOfTypeAll<SceneView>();
            if (sceneWindows != null && sceneWindows.Length > 0) sceneWindow = sceneWindows[0];
        }
        
        static readonly System.Type gameWindowType = typeof(EditorWindow).Assembly.GetType("UnityEditor.PlayModeView");
        
        static void FindGameWindow() 
        {
            if (gameWindow != null) return;
            var gameWindows = Resources.FindObjectsOfTypeAll(gameWindowType);
            if (gameWindows != null && gameWindows.Length > 0) gameWindow = (EditorWindow)gameWindows[0];
        }
        
        static void StoreSceneNeedFocus() 
        {
            FindSceneWindow();
            sceneNeedFocus = sceneWindow != null && sceneWindow.hasFocus;
        }
        
        private static void OnPlayModeStateChanged(PlayModeStateChange stateChange) 
        {
            if (disabled) return;
        
            switch (stateChange) 
            {
                case PlayModeStateChange.ExitingEditMode:
                    StoreSceneNeedFocus();
                    break;
                case PlayModeStateChange.EnteredPlayMode:
                    EditorApplication.pauseStateChanged += OnPauseStateChanged;
                    if (EditorSettings.enterPlayModeOptionsEnabled &&
                        EditorSettings.enterPlayModeOptions.HasFlag(EnterPlayModeOptions.DisableDomainReload)) 
                    {
                        if (sceneNeedFocus) FocusSceneWindow();
                    } 
                    else 
                    {
                        if (forceFocusSceneOnPlay) FocusSceneWindow();
                    }
                    break;
                case PlayModeStateChange.ExitingPlayMode:
                    StoreSceneNeedFocus();
                    EditorApplication.pauseStateChanged -= OnPauseStateChanged;
                    break;
                case PlayModeStateChange.EnteredEditMode:
                    FindSceneWindow();
                    if (sceneWindow != null) 
                    {
                        if (sceneNeedFocus) 
                        {
                            if (!sceneWindow.hasFocus) FocusSceneWindow();
                        } 
                        else
                        {
                            FindGameWindow();
                            if (gameWindow != null) FocusGameWindow();
                        }
                    }
                    break;
            }
        }
        
        static void OnPauseStateChanged(PauseState pauseState) 
        {
            if (disabled) return;
        
            switch (pauseState) 
            {
                case PauseState.Paused:
                    StoreSceneNeedFocus();
                    if (!sceneNeedFocus) 
                    {
                        FindGameWindow();
                        if (gameWindow != null && gameWindow.hasFocus) 
                            FocusOnUpdate(FocusGameWindow);
                    } 
                    else FocusOnUpdate(FocusSceneWindow);
                    break;
                case PauseState.Unpaused:
                    if (sceneNeedFocus) FocusOnUpdate(FocusSceneWindow);
                    break;
            }
        }
        
        static void FocusOnUpdate(System.Action onFocus) 
        {
            KeepSceneFocused.onFocus = onFocus;
            oneFrameSkipped = false;
            EditorApplication.update += OnUpdateFocus;
        }
        
        static void OnUpdateFocus() 
        {
            if (oneFrameSkipped) 
            {
                EditorApplication.update -= OnUpdateFocus;
                onFocus();
            }
            oneFrameSkipped = true;
        }
        
        static void FocusSceneWindow() 
        {
            FindSceneWindow();
            if (sceneWindow != null) sceneWindow.Focus();
        }
        
        static void FocusGameWindow() 
        {
            FindGameWindow();
            if (gameWindow != null) gameWindow.Focus();
        }
    }
}
#endif
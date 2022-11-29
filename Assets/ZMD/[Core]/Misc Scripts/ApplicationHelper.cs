using UnityEngine;
using UnityEngine.Events;

namespace ZMD
{
    public class ApplicationHelper : MonoBehaviour
    {
        [SerializeField] UnityEvent OnQuit;
    
        public void Quit()
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
        }
        
        public void OnApplicationQuit() => OnQuit.Invoke();
    }
}
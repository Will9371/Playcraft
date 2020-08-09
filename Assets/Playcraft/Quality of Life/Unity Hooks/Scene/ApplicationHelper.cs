using UnityEngine;

namespace Playcraft
{
    public class ApplicationHelper : MonoBehaviour
    {
        public void Quit()
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
        }
    }
}
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Playcraft.Scene
{
    public class SingleSceneController : MonoBehaviour
    {
        public void SetScene(string name)
        {
            SceneManager.LoadScene(name);
        }

        public void ResetScene()
        {
            UnityEngine.SceneManagement.Scene scene = SceneManager.GetActiveScene();
            SetScene(scene.name);
        }
    }
}


using UnityEngine;
using UnityEngine.SceneManagement;

namespace ZMD
{
    [CreateAssetMenu(menuName = "ZMD/Scenes/Scene Grid")]
    public class SceneGridActions : GridActions
    {
        public override void Add(Object value) { SceneManager.LoadScene(value.name, LoadSceneMode.Additive); }
        public override void Remove(Object value) { SceneManager.UnloadSceneAsync(value.name); }
    }
}

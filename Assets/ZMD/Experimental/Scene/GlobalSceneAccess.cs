using UnityEngine;

namespace ZMD
{
    public class GlobalSceneAccess : MonoBehaviour
    {
        GlobalScenes scenes => GlobalScenes.instance;
        public void Enter(Object scene) { scenes.Enter(scene); }
    }
}

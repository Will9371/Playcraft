using UnityEngine;

// Make any class into a singleton by inheriting from this class
// Format: public class ExampleClass : Singleton<ExampleClass>
namespace Playcraft
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected static T _instance;

        public static T instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = (T)FindObjectOfType(typeof(T));

                    if (_instance == null)
                        Debug.LogError("An instance of " + typeof(T) + " is needed in the scene, but there is none");
                }

                return _instance;
            }
        }

        [SerializeField] bool dontDestroyOnLoad = false;

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }
        
            if (dontDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}

using UnityEngine;

// DOCUMENT: Describe purpose in VR scene as example
namespace Playcraft
{
    public class LockInteractor : MonoBehaviour
    {
        public void Lock(GameObject other)
        {
            var interactor = other.GetComponent<Interactor>();
            if (!interactor) return;
            interactor.SetLock(true);
        }
        
        public void Unlock(GameObject other)
        {
            var interactor = other.GetComponent<Interactor>();
            if (!interactor) return;
            interactor.SetLock(false);
        }
    }
}

using UnityEngine;

namespace Playcraft
{
    public class LockInteractor : MonoBehaviour
    {
        public void Lock(GameObject other)
        {
            var interactor = other.GetComponent<Interact>();
            if (!interactor) return;
            interactor.SetLock(true);
        }
        
        public void Unlock(GameObject other)
        {
            var interactor = other.GetComponent<Interact>();
            if (!interactor) return;
            interactor.SetLock(false);
        }
    }
}

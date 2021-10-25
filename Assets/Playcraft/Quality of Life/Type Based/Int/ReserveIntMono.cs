using UnityEngine;

namespace Playcraft
{
    /// Allows multiple sources to get non-overlapping integers from a range
    public class ReserveIntMono : MonoBehaviour
    {
        [SerializeField] ReserveInt process;
        public int GetRandomAvailable(int sourceId) { return process.GetRandomAvailable(sourceId); }
        public bool SetIfAvailable(int value, int sourceId) { return process.SetIfAvailable(value, sourceId); }
        public int SetIfAvailableOrGetRandom(int value, int sourceId) { return process.SetIfAvailableOrGetRandom(value, sourceId); }
    }
}

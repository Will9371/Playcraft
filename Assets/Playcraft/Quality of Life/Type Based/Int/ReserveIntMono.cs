using UnityEngine;

namespace Playcraft
{
    /// Allows multiple sources to get non-overlapping integers from a range
    public class ReserveIntMono : MonoBehaviour
    {
        [SerializeField] ReserveInt process;
        public int GetAvailableValue(int sourceId) { return process.GetAvailableValue(sourceId); }
    }
}

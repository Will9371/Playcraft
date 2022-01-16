using UnityEngine;

namespace Playcraft
{
    [CreateAssetMenu(menuName = "Playcraft/Data Types/Object Array")]
    public class ObjectArraySO : ScriptableObject
    {
        public GameObject[] Values;

        public GameObject GetRandom()
        {
            var index = Random.Range(0, Values.Length);
            return Values[index];
        }
    }
}

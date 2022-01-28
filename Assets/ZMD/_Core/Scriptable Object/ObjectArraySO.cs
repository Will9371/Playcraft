using UnityEngine;

namespace ZMD
{
    [CreateAssetMenu(menuName = "ZMD/Data Types/Object Array")]
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

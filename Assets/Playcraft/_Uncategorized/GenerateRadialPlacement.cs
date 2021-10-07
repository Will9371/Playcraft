using UnityEngine;

namespace Playcraft
{
    public class GenerateRadialPlacement : MonoBehaviour
    {
        [SerializeField] Transform centerPoint;
        [SerializeField] GameObject prefab;
        [SerializeField] int count = 4;
        [SerializeField] RadialPlacement placement;
        
        [HideInInspector] public Transform[] placedPrefabs;

        void Start() { Generate(); }

        public void Generate()
        {
            if (placedPrefabs != null)
                foreach (var item in placedPrefabs)
                    Destroy(item.gameObject);
        
            placedPrefabs = new Transform[count];
        
            for (int i = 0; i < count; i++)
                placedPrefabs[i] = Instantiate(prefab, centerPoint).transform;

            placement.Place(placedPrefabs);
        }
        
        public void SetActive(bool value)
        {
            if (placedPrefabs == null) return;
            foreach (var item in placedPrefabs)
                item.gameObject.SetActive(value);
        }
    }
}

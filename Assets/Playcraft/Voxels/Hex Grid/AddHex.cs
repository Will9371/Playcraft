using System.Collections;
using UnityEngine;

namespace Playcraft.Voxels
{
    public class AddHex : MonoBehaviour
    {
        public HexMap map;
        [SerializeField] Vector3Array directions;
        [SerializeField] SO hexTag;
        public bool isReal;
        
        bool addFlag;
        
        public void Add() 
        {
            if (addFlag) return;
            addFlag = true; 
            map.AddHex(gameObject); 
        }
        
        public void RefreshAdders() 
        {
            if (Application.isPlaying) 
                StartCoroutine(RefreshAdderRoutine()); 
            else
                foreach (var direction in directions.values)
                    RequestPlace(transform.position + direction * transform.localScale.x);
        }
        
        IEnumerator RefreshAdderRoutine()
        {
            foreach (var direction in directions.values)
            {
                RequestPlace(transform.position + direction * transform.localScale.x); 
                yield return null;   
            }    
        }

        void RequestPlace(Vector3 position)
        {
            var overlap = Physics.OverlapSphere(position, 0.1f);
            var freeSpace = true;
            
            foreach (var item in overlap)
            {
                var tags = item.GetComponent<CustomTags>();
                if (!tags || !tags.HasTag(hexTag)) continue;
                freeSpace = false;
            }
            
            if (freeSpace)
                map.AddHexOption(position);
        }
    }
}

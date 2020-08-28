// CREDIT: c00pala
// YouTube series: https://www.youtube.com/playlist?list=PLL8DeMf3fIgFSJj6OTxXceaMtD5vWFER5

using System.Collections.Generic;
using UnityEngine;

namespace Playcraft
{
    public class ItemListController : MonoBehaviour 
    {
        #pragma warning disable 0649
        [SerializeField] RectTransform content;
        [SerializeField] GameObject itemTemplate;
        #pragma warning restore 0649
            
        // Stores size of item template
        RectTransform itemRect;
        
        public List<GameObject> items = new List<GameObject>();
        
        private void Start()
        {
            itemTemplate.SetActive(false);       
        }
        
        public void Create(int count)
        {
            for (int i = 0; i < count; i++)
                Create();
        }
        
        public GameObject Create()
        {
            var newItem = Instantiate(itemTemplate, content);
            newItem.SetActive(true);    
            items.Add(newItem);
            return newItem;
        }
        
        public void Remove(GameObject item)
        {
            Remove(GetItemIndex(item));
        }
        
        public void Remove(int index)
        {
            if (index >= items.Count)
                return;
            
            Destroy(items[index].gameObject);
            items.Remove(items[index]);     
        }
        
        public void Clear()
        {
            for (int i = items.Count - 1; i >= 0; i--)
                Remove(i);
        }
        
        // Sort transforms in the same order as the internal object list
        public void Sort()
        {        
            for (int i = 0; i < items.Count; i++)
                items[i].transform.SetSiblingIndex(i + 1);            
        }
        
        public Vector3 GetItemPosition(int index)
        {
            if (items.Count <= 0)
            {
                Debug.LogError("Item list empty");
                return Vector3.zero;
            }
            if (index != 0 && (index >= items.Count || index < 0))
            {
                Debug.LogError("Index " + index + " out of range " + items.Count);
                return Vector3.zero;
            }
            
            //Debug.Log(index + ", " + items[index].transform.position);
            return items[index].transform.position;
        }
        
        public int GetItemIndex(GameObject item)
        {
            return items.IndexOf(item);
        }
    }
}

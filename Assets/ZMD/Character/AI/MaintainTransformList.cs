using System.Collections.Generic;
using UnityEngine;

namespace ZMD
{
    // RENAME: Describe what is being done with the list
    public class MaintainTransformList : MonoBehaviour
    {    
        List<Transform> items = new();
        
        [SerializeField] TransformListEvent OnUpdateList;
            
        public void AddItem(Collider item) { AddItem(item.transform); }
        public void RemoveItem(Collider item) { RemoveItem(item.transform); }
            
        public void AddItem(Transform item)
        {        
            if (!items.Contains(item))
                items.Add(item);
                
            OnUpdateList.Invoke(items);
        }
        
        public void RemoveItem(Transform item)
        {
            items.Remove(item);         
            OnUpdateList.Invoke(items);
        }
    }
}

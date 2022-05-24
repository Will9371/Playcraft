using UnityEngine;

namespace ZMD
{
    [CreateAssetMenu(menuName = "ZMD/Scenes/Object Grid")]
    public class ObjectGridActions : GridActions
    {
        public override void Add(Object value) 
        {
            var gameObject = value as GameObject; 
            if (!gameObject) return;
            gameObject.SetActive(true); 
        }
        
        public override void Remove(Object value) 
        { 
            var gameObject = value as GameObject; 
            if (!gameObject) return;
            gameObject.SetActive(false);  
        }
    }
}
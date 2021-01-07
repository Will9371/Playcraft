// INCOMPLETE


using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Playcraft.Examples.Saving
{
    public class SaveShape : MonoBehaviour
    {
        /*public ShapeType shapeType;
        public ShapeData shapeData;
        
        SaveData saveData => SaveData.current;
        
        
        string date => DateTime.Now.ToLongDateString();
        string time => DateTime.Now.ToLongTimeString();
        
        public void Start()
        {
            if (string.IsNullOrEmpty(shapeData.id))
            {
                shapeData.id = date + time + Random.Range(0, int.MaxValue);
                shapeData.shapeType = shapeType;
                saveData.shapes.Add(shapeData);
            }
            
            GameEvents.current.onLoadEvent += DestroyMe;
        }
        
        void Update()
        {
            shapeData.position = transform.position;
            shapeData.rotation = transform.rotation;
        }
        
        void DestroyMe()
        {
            GameEvents.current.onLoadEvent -= DestroyMe;
            Destroy(gameObject);
        }*/
    }
}


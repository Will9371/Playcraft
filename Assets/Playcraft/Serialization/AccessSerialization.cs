using System.Collections.Generic;
using UnityEngine;
using Playcraft.Examples.Saving; 

// For your project create a custom SaveData class
// and remove the Playcraft.Examples.Saving using statement above
namespace Playcraft
{
    public class AccessSerialization : MonoBehaviour
    {
        //[SerializeField] SpawnShape spawner;
        
        SaveData saveData;
        
        //void Awake() { saveData = SaveData.current; }

        public void Load()
        {
            //GameEvents.dispatchOnLoadEvent?.Invoke();
        
            SaveData.current = (SaveData)SerializationManager.Load();
            
            // INCOMPLETE
            /*for (int i = 0; i < saveData.shapes.Count; i++)
            {
                var currentShape = saveData.shapes[i];
                var obj = spawner.Spawn(currentShape.shapeType);
                var saveShape = obj.GetComponent<SaveShape>();
                saveShape.shapeData = currentShape;
                saveShape.transform.position = currentShape.position;
                saveShape.transform.rotation = currentShape.rotation;
            }*/
        }
        
        public void Save()
        {
            SerializationManager.Save();
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Playcraft.Voxels
{
    public class HexMap : MonoBehaviour
    {
        [SerializeField] GameObject hex;
        [SerializeField] GameObject addHex;
        [SerializeField] bool createAddersOnStart;
        [SerializeField] float scale = 1f;
        
        [SerializeField] List<GameObject> hexes = new List<GameObject>();
        [SerializeField] List<GameObject> addableHexes = new List<GameObject>();
        
        Vector3 uniformScale => new Vector3(scale, scale, scale);
        
        void Start()
        {
            if (createAddersOnStart)
                RefreshAllAdders();
        }
        
        public void RefreshAllAdders()
        {
            foreach (var item in hexes)
            {
                item.transform.localScale = uniformScale;
                item.GetComponent<AddHex>().RefreshAdders();
            }
        }

        public void AddHex(GameObject added)
        {
            var newHex = Instantiate(hex, transform);
            newHex.transform.localScale = uniformScale;
            newHex.transform.position = added.transform.position;
            hexes.Add(newHex);
            
            var addLogic = newHex.GetComponent<AddHex>();
            addLogic.map = this;
            addLogic.RefreshAdders();

            addableHexes.Remove(added);
            StartCoroutine(DestroyHex(added));
        } 
        
        public void AddHexOption(Vector3 position)
        {
            var newOption = Instantiate(addHex, transform);
            newOption.transform.localScale = uniformScale;
            newOption.transform.position = position;
            
            addableHexes.Add(newOption);
            var addLogic = newOption.GetComponent<AddHex>();
            addLogic.map = this;
        }   
        
        IEnumerator DestroyHex(GameObject hex)
         {
             yield return null;
             DestroyImmediate(hex);
         }
    }
}

/*
     bool IsHexAtPosition(GameObject newHexObj)
     {
         var position = newHexObj.transform.position;
         var newHex = newHexObj.GetComponent<AddHex>();

         foreach (var item in hexes)
             if (item.transform.position == position)
                 return true;
         foreach (var item in addableHexes)
             if (item.transform.position == position && !newHex.isReal)
             {
                 Debug.Log(newHexObj.name);
                 return true;
             }

         return false;
     }
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZMD.Voxels
{
    public class HexMap : MonoBehaviour
    {
        [SerializeField] GameObject hex;
        [SerializeField] GameObject addHex;
        [SerializeField] bool createAddersOnStart;
        [SerializeField] float scale = 1f;
        
        [SerializeField] List<GameObject> hexes = new();
        [SerializeField] List<GameObject> addableHexes = new();
        
        Vector3 uniformScale => new(scale, scale, scale);
        
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
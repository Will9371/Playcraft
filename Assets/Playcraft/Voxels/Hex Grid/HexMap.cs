using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    //[SerializeField] int radius;
    //[SerializeField] bool generate;
    
    /*void OnValidate()
    {
        if (generate)
        {
            Clear();
            Generate();
            generate = false;
        }
    }
    
    void Generate()
    {
        for (int i = 0; i < radius; i++)
        {
            var newHex = Instantiate(hexPrefab, transform);
            hexes.Add(newHex);
        }
    }

    void Clear()
    {
        foreach (var hex in hexes)
            StartCoroutine(DestroyHex(hex));
        
        hexes.Clear();
    }
    
*/
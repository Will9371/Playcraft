using UnityEngine;
using Playcraft;

public class AddHex : MonoBehaviour
{
    public HexMap map;
    [SerializeField] Vector3Array directions;
    [SerializeField] SO hexTag;
    public bool isReal;

    public void Add() { map.AddHex(gameObject); }
    
    public void RefreshAdders()
    {
        foreach (var direction in directions.values)
            RequestPlace(transform.position + direction * transform.localScale.x);
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

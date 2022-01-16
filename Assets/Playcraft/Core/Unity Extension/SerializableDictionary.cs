// CREDIT: christophfranke123, modified by Will Petillo
// SOURCE: https://answers.unity.com/questions/460727/how-to-serialize-dictionary-with-unity-serializati.html

using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField] List<KeyValue> keyValues;
    
    // Not used, satisfies interface requirement
    public void OnBeforeSerialize() { }
 
    // Load dictionary from lists
    public void OnAfterDeserialize()
    {
        Clear();
        
        for(int i = 0; i < keyValues.Count; i++)
            Add(keyValues[i].key, keyValues[i].value);
    }
    
    [Serializable] 
    public struct KeyValue
    {
        public TKey key;
        public TValue value;
        
        public KeyValue(TKey key, TValue value)
        {
            this.key = key;
            this.value = value;
        }
    }
}

/*
// EXAMPLE USAGE

DataStruct data;

[Serializable] public class LookupDictionary : SerializableDictionary<EnumId, DataStruct> { }
[SerializeField] LookupDictionary lookup;
        
public void SetData(EnumId id) { lookup.TryGetValue(id, out data); }
*/

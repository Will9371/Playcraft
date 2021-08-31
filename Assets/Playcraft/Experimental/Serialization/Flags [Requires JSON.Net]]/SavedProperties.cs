// Requires Json.Net for Unity

/*
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable, JsonObject(MemberSerialization.OptIn)]
public class SavedProperties
{
    [SerializeField, JsonProperty]
    private List<BoolSavedProperty> boolProperties = new List<BoolSavedProperty>();

    public void SetBool(string name, bool value)
    {
        GetOrInitBoolProperty(name).Value = value;
    }

    public bool GetBool(string name)
    {
        return GetOrInitBoolProperty(name).Value;
    }

    BoolSavedProperty GetOrInitBoolProperty(string name)
    {
        var property = boolProperties.FirstOrDefault(
            p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

        if (property == null)
        {
            property = new BoolSavedProperty(name);
            boolProperties.Add(property);
        }

        return property;
    }
}
*/
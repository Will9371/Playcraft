using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Architecture/Global Game Object")]
public class GlobalGameObject : ScriptableObject
{
    private GameObject obj;

    public void Assign(GameObject gameObject)
    {
        obj = gameObject;
    }

    public GameObject Obj()
    {
        return obj;
    }
}

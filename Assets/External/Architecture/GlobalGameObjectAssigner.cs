using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// With this you can get a reference to any GameObject via the inspector. 
// 1) Create a GlobalGameObject in Project Window
// 2) Add GlobalGameObjectAssigner component onto the object you want to reference
// 3) When you need access to the object, create a public GlobalGameObject field, and drop/drop the Scriptable Object into the field.

public class GlobalGameObjectAssigner : MonoBehaviour
{

    public GlobalGameObject globalGameObject;

    private void Awake()
    {
        globalGameObject.Assign(this.gameObject);
    }

}

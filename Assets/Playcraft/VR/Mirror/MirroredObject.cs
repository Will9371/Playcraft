// Credit: TheVirtualMunk
// Source: https://forum.unity.com/threads/mirror-reflections-in-vr.416728/
// Edited by Will Petillo

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Playcraft;
 
[ExecuteInEditMode]
public class MirroredObject : MonoBehaviour
{
    enum MirrorType { Plane, Point }
    [SerializeField] MirrorType mirrorType = MirrorType.Plane;

    public bool executeInEditor = true;
    public Transform mirror;
    public int mirrorWorldLayer;
    public bool mirrorRoot = true;
    public bool syncChildren = true;
 
    public GameObject mirroredObject;
 
    [SerializeField] List<Transform> mirroredTransformChilds, originTransformChilds;
    [SerializeField] GameObjectSO miroredObjectsHolderReference;
    GameObject mirroredObjectsHolder;
    
    [SerializeField] UnityEvent OnClone;
    
    Plane mirrorPlane;

 
    void Update()
    {
        if (!executeInEditor && Application.isEditor)
            return;
 
        if (!mirroredObject && mirror)
            InstantiateClone();

        if(mirroredObject && mirror)
            MirrorObject();
    }
    void Start()
    {
        if (!mirroredObject && mirror)
            InstantiateClone();
    }
 
    void InstantiateClone()
    {
        // Clear previous instances
        DeleteMirrorInstance();
        
        mirroredObjectsHolder = miroredObjectsHolderReference.value;

        // Create Instance
        mirroredObject = Instantiate(gameObject, mirroredObjectsHolder.transform);
     
        // Mirror X Axis
        mirroredObject.transform.localScale = new Vector3(mirroredObject.transform.localScale.x * -1, mirroredObject.transform.localScale.y, mirroredObject.transform.localScale.z);
 
        // Removes this script from the cloned object
        mirroredObject.GetComponent<MirroredObject>().enabled = false;
        DestroyImmediate(mirroredObject.GetComponent<MirroredObject>());

        // Setup lists for mirroring child transforms
        originTransformChilds = new List<Transform>(GetComponentsInChildren<Transform>(true));
        mirroredTransformChilds = new List<Transform>(mirroredObject.GetComponentsInChildren<Transform>(true));
 
        // Assign layer to mirror world
        mirroredObject.layer = mirrorWorldLayer;
        foreach (var item in mirroredObject.GetComponentsInChildren<Transform>(true))
            item.gameObject.layer = mirrorWorldLayer;
            
        OnClone.Invoke();
    }
 
    void OnDisable()
    {
        DeleteMirrorInstance();
    }
 
    void DeleteMirrorInstance()
    {
       if (mirroredObject)
            DestroyImmediate(mirroredObject);
    }
 
    void MirrorObject()
    {
        mirrorPlane = new Plane(mirror.forward, mirror.position);
 
        Vector3 closestPoint = Vector3.zero;
        float distanceToMirror = 0f;
        Vector3 mirrorPos = Vector3.zero;
 
        if (syncChildren)
            SyncChildren(ref closestPoint, ref distanceToMirror, ref mirrorPos);

        if (!mirrorRoot)
            return;
        
        switch (mirrorType)
        {
            case MirrorType.Plane: 
                MirrorOnPlane(transform, mirroredObject.transform, ref closestPoint, ref distanceToMirror, ref mirrorPos);
                break;
            case MirrorType.Point:
                MirrorOnPoint(transform, mirroredObject.transform);
                break;
        }
    }
    
    void SyncChildren(ref Vector3 closestPoint, ref float distanceToMirror, ref Vector3 mirrorPos)
    {
        for (int i = originTransformChilds.Count - 1; i >= 0; i--)
        {
            // WPP: allow for culling of objects from mirrored object
            if (!originTransformChilds[i])
                originTransformChilds.Remove(originTransformChilds[i]);
            if (i < mirroredTransformChilds.Count)
                continue;
            if (!mirroredTransformChilds[i])
            {
                mirroredTransformChilds.Remove(mirroredTransformChilds[i]);
                continue;
            }
            
            switch (mirrorType)
            {
                case MirrorType.Plane: 
                    MirrorOnPlane(transform, mirroredObject.transform, ref closestPoint, ref distanceToMirror, ref mirrorPos);
                    break;
                case MirrorType.Point:
                    MirrorOnPoint(transform, mirroredObject.transform);
                    break;
            }
        }
    }
    
    void MirrorOnPlane(Transform original, Transform mirrored, ref Vector3 closestPoint, ref float distanceToMirror, ref Vector3 mirrorPos)
    {
        closestPoint = mirrorPlane.ClosestPointOnPlane(original.position);
        distanceToMirror = mirrorPlane.GetDistanceToPoint(original.position);
        mirrorPos = closestPoint - mirrorPlane.normal * distanceToMirror;
 
        mirrored.position = mirrorPos;
        mirrored.rotation = ReflectRotation(original.rotation, mirrorPlane.normal);
    }
    
    Vector3 originalToCenter;
    Vector3 mirroredPosition;
    
    void MirrorOnPoint(Transform original, Transform mirrored)
    {
        originalToCenter = original.position - mirror.position;
        mirroredPosition = mirror.position - originalToCenter;
        mirroredPosition.y = original.position.y;
        
        mirrored.position = mirroredPosition;
        mirrored.rotation = ReflectRotation(original.rotation, originalToCenter.normalized);
    }

    Quaternion ReflectRotation(Quaternion source, Vector3 normal)
    {
        return Quaternion.LookRotation(Vector3.Reflect(source * Vector3.forward, normal), Vector3.Reflect(source * Vector3.up, normal));
    }
 
    #region Debug
    #if UNITY_EDITOR
    void OnDrawGizmos()
    {
        if (!mirror) return;
 
        Gizmos.color = Color.blue;
        Vector3 closestPoint = mirrorPlane.ClosestPointOnPlane(transform.position);
        Gizmos.DrawLine(transform.position, closestPoint);
 
        Gizmos.color = Color.cyan;
        Vector3 mirrorPos = closestPoint - mirrorPlane.normal * -1;
        Gizmos.DrawLine(closestPoint, mirrorPos);
 
        Gizmos.color = Color.green;
        Gizmos.DrawRay(new Ray(mirror.position, mirror.up));
 
        Gizmos.color = Color.red;
        Gizmos.DrawRay(new Ray(mirror.position, mirror.forward * -1));
 
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(new Ray(mirror.position, mirrorPlane.normal));
    }
    #endif
    #endregion
}
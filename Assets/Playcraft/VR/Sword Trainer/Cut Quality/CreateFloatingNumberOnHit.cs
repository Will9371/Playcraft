using UnityEngine;
using Playcraft;
using Playcraft.Pooling;


public class CreateFloatingNumberOnHit : MonoBehaviour, ISwingTarget
{
    [SerializeField] GameObject floaterPrefab;
    [SerializeField] Transform canvas;
    [SerializeField] Transform faceTarget;
    [SerializeField] RectTransform template;
    [SerializeField] FloatEvent RelayScore;
    
    ObjectPoolMaster spawner => ObjectPoolMaster.instance;
    
    public void SetFloaterCanvas(Transform value) { canvas = value; }
    public void SetFloaterFaceTarget(Transform value) { faceTarget = value; }

    public void SendData(SwingData data)
    {
        var score = Mathf.Pow(data.speed * data.edgeAlignment * 10f, 2f);
        
        var floaterObj = spawner.Spawn(floaterPrefab, transform.position);
        floaterObj.transform.SetParent(canvas);
        floaterObj.SetActive(true);
        
        var rect = floaterObj.transform as RectTransform;
        rect.localRotation = template.localRotation;
        rect.localScale = template.localScale;
        
        var floater = floaterObj.GetComponent<FloatingNumber>();      
        floater.Begin(transform, data.direction, score);
        
        if (faceTarget)
        {
            var facing = floaterObj.GetComponent<FaceTargetInstant>();
            facing.SetTarget(faceTarget);
        }
        
        RelayScore.Invoke(score);
    }
}

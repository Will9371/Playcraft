using System;
using UnityEngine;

// GENERALIZE: consider replacing enum with SOs
public class SwordPracticeSceneDev : MonoBehaviour
{
    public enum SetupId { CirclingTargets, HPTarget, MultipleOpponents, CutOnly, ParryOnly, Shadow, CutParryShadow, CutAndParry }
    public SetupId setupId;
    
    [SerializeField] Setup[] setups;

    void OnValidate()
    {
        if (!gameObject.activeInHierarchy)
            return;
            
        Refresh();
    }
    
    void Refresh()
    {
        foreach (var setup in setups)
            setup.SetActive(false);
            
        GetSetup().SetActive(true);
    }
    
    Setup GetSetup()
    {
        foreach (var setup in setups)
            if (setup.id == setupId)
                return setup;
        
        return null;
    }
    
    [Serializable] public class Setup
    {
        public SetupId id;
        public GameObject[] activeObjects;
        
        public void SetActive(SetupId id) { SetActive(this.id == id); }
        
        public void SetActive(bool value)
        {
            foreach (var obj in activeObjects)
                obj.SetActive(value);
        }
    }
}

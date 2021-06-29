using System;
using UnityEngine;

// GENERALIZE: consider replacing enum with SOs
public class SwordPracticeSceneDev : MonoBehaviour
{
    public enum SetupId { CirclingTargets, HPTarget, CutAndParry, CutOnly, ParryOnly }
    public SetupId setupId;
    
    [SerializeField] Setup[] setups;

    void OnValidate()
    {
        Refresh();
    }
    
    void Refresh()
    {
        foreach (var setup in setups)
            setup.SetActive(setupId);
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
        
        public void SetActive(SetupId id)
        {
            var isActive = this.id == id;
            
            foreach (var obj in activeObjects)
                obj.SetActive(isActive);
        }
    }
}

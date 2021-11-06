using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Playcraft
{
    /// Simplifies access of post processing vignette settings
    [Serializable]
    public class AccessVignette
    {
        [SerializeField] PostProcessVolume volume;
        
        Vignette _vignette;
        Vignette vignette
        {
            get
            {
                if (!_vignette)
                    volume.profile.TryGetSettings(out _vignette); 
                    
                return _vignette;
            }
        }
        
        public float intensity
        {
            get => vignette.intensity.value;
            set => vignette.intensity.value = value;
        }
        
        public Color color
        {
            get => vignette.color.value;
            set => vignette.color.value = value;
        }
    }
}

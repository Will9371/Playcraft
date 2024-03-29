using System;
using System.Collections;
using UnityEngine;

namespace ZMD
{
    public class FlashColorMono : MonoBehaviour
    {
        [SerializeField] FlashColor process;
        void Start() { process.Start(); }
        public void BeginFlash() { StartCoroutine(process.Flash()); }
        public void BeginFlash(int flashCount) { StartCoroutine(process.Flash(flashCount)); }
    }
    
    [Serializable]
    public class FlashColor
    {
        public Renderer rend;
        public Color flashColor;
        public float cycleTime;
        public int defaultFlashCount;
        
        Color startColor;
        public void Start() { startColor = rend.material.color; }
        
        public void SetColor(Color value)
        {
            startColor = value;
            rend.material.color = value;
        }

        public IEnumerator Flash() { yield return Flash(defaultFlashCount); }
        public IEnumerator Flash(int flashCount)
        {
            var delay = new WaitForSeconds(cycleTime/2f);
                
            for (int i = 0; i < flashCount; i++)
            {
                rend.material.color = flashColor;
                yield return delay;
                rend.material.color = startColor;
                yield return delay;
            }
        }
    }
}

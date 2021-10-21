using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Playcraft
{
    public class FlashColor : MonoBehaviour
    {
        [SerializeField] Renderer rend;
        [SerializeField] Color defaultColor;
        [SerializeField] Color flashColor;
        [SerializeField] float cycleTime;
        [SerializeField] int defaultFlashCount;
        
        public void BeginFlash() { BeginFlash(defaultFlashCount); }
        public void BeginFlash(int flashCount) { StartCoroutine(Flash(flashCount)); }

        IEnumerator Flash(int flashCount)
        {
            var delay = new WaitForSeconds(cycleTime/2f);
                
            for (int i = 0; i < flashCount; i++)
            {
                rend.material.color = flashColor;
                yield return delay;
                rend.material.color = defaultColor;
                yield return delay;
            }
        }
    }
}

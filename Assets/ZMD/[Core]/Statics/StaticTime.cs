using System.Collections;
using UnityEngine;

namespace ZMD
{
    public static class StaticTime
    {
        public static IEnumerator Flash(Renderer renderer, Color[] colorSequence, float flashTime, int flashCycles)
        {
            var delay = new WaitForSeconds(flashTime);
            
            for (int i = 0; i < flashCycles; i++)
            {
                foreach (var color in colorSequence)
                { 
                    renderer.material.color = color; 
                    yield return delay;
                }
            }
        }
    }
}

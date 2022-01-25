// CREDIT: guavaman, AnneCHPostma, DeloitteCam, & Sushi271   
// SOURCE: https://forum.unity.com/threads/find-out-which-key-was-pressed.385250/
// Modified by Will Petillo

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ZMD
{
    public static class GetKey
    {
        static readonly KeyCode[] keyCodes =
            Enum.GetValues(typeof(KeyCode))
                .Cast<KeyCode>()
                .Where(k => (int)k < (int)KeyCode.Mouse0)
                .ToArray();
     
        public static IEnumerable<KeyCode> GetCurrentKeysDown()
        {
            if (!Input.anyKeyDown) yield break;
            
            foreach (var key in keyCodes)
                if (Input.GetKeyDown(key))
                    yield return key;
        }
     
        public static IEnumerable<KeyCode> GetCurrentKeys()
        {
            if (!Input.anyKey) yield break;
            
            foreach (var key in keyCodes)
                if (Input.GetKey(key))
                    yield return key;
        }
     
        public static IEnumerable<KeyCode> GetCurrentKeysUp()
        {
            foreach (var key in keyCodes)
                if (Input.GetKeyUp(key))
                    yield return key;
        }
    }
}
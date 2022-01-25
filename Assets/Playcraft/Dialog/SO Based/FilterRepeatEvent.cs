using System.Collections.Generic;
using UnityEngine;

namespace ZMD
{
    public class FilterRepeatEvent : MonoBehaviour
    {
        List<SO> list = new List<SO>();
        [SerializeField] TagEvent OnAdd = default;
        
        public void Input(SO value)
        {
            if (list.Contains(value)) return;
            list.Add(value);
            OnAdd.Invoke(value);
        }
    }
}

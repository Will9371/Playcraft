using System.Collections.Generic;
using UnityEngine;

namespace Playcraft
{
    public class FilterRepeatEvent : MonoBehaviour
    {
        List<TagSO> list = new List<TagSO>();
        [SerializeField] TagEvent OnAdd = default;
        
        public void Input(TagSO value)
        {
            if (list.Contains(value)) return;
            list.Add(value);
            OnAdd.Invoke(value);
        }
    }
}

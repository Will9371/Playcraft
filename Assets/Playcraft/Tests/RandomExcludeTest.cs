using System.Collections.Generic;
using UnityEngine;

namespace Playcraft.Examples
{
    // Tests RandomStatics.RandomIndexNotIncluding
    public class RandomExcludeTest : MonoBehaviour
    {
        [SerializeField] int max;
        [SerializeField] List<int> exclude;
        [SerializeField] int prior;
        [SerializeField] int result;
        [SerializeField] bool trigger;
        
        List<int> _exclude = new List<int>();
        
        void OnValidate()
        {
            if (trigger)
            {
                Refresh();
                trigger = false;
            }
        }
        
        void Refresh()
        {
            prior = result;
            
            _exclude.Clear();
            foreach (var item in exclude)
                _exclude.Add(item);
                
            if (!_exclude.Contains(prior))
                _exclude.Add(prior);
            
            result = RandomStatics.RandomIndexNotIncluding(max, _exclude);
        }
    }
}

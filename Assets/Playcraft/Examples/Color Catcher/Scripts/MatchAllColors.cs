using UnityEngine;

namespace Playcraft.Examples.ColorCatcher
{
    public class MatchAllColors : MonoBehaviour
    {
        BlockerData[] priorData; 

        bool changeFlag;

        public void Set(int index)
        {
            if (changeFlag) return;
            changeFlag = true;

            var data = transform.GetChild(index).GetComponent<WispBlockerOverride>().data;
    
            var count = transform.childCount;
            priorData = new BlockerData[count];
    
            for (int i = 0; i < count; i++)
                priorData[i] = transform.GetChild(i).GetComponent<WispBlockerOverride>().data;

            foreach (Transform sphere in transform)
                sphere.GetComponent<WispBlockerOverride>().Set(data);
        }

        public void Clear()
        {
            for (int i = 0; i < transform.childCount; i++)
                transform.GetChild(i).GetComponent<WispBlockerOverride>().Set(priorData[i]);
       
            changeFlag = false;
        }
    }      
}

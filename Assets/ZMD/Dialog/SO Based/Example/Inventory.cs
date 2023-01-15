using UnityEngine;
using TMPro;

namespace ZMD.Examples.Dialog
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] TMP_Text coinText;
        int coinCount;
    
        public void FindSecret()
        {
            coinCount++;
            coinText.text = coinCount.ToString();
        }
    }
}

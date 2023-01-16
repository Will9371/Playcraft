using System;
using UnityEngine;

namespace ZMD.Dialog
{
    public class ResponseButtons : MonoBehaviour
    {
        [SerializeField] ResponseButton[] responses;
        
        void Start()
        {
            foreach (var response in responses)
            {
                response.onClick = OnClick;
                response.gameObject.SetActive(false);
            }
        }

        void OnClick(int id) 
        {
            foreach (var response in responses)
                response.gameObject.SetActive(false);
                 
            onClick?.Invoke(id); 
        }
        public Action<int> onClick;

        public void Refresh(DialogNode node)
        {
            var nodeResponseCount = node.responses.Length;
        
            for (int i = 0; i < responses.Length; i++)
            {
                var withinList = i < nodeResponseCount;
                responses[i].gameObject.SetActive(withinList);
                if (withinList) responses[i].SetText(node.responses[i].response);
            }
        }
        
        void OnValidate()
        {
            for (int i = 0; i < responses.Length; i++)
                responses[i].responseId = i;
        }
    }
}

﻿using UnityEngine;
using UnityEngine.UI;

namespace ZMD.Dialog
{
    public class SimpleDialogDisplay : MonoBehaviour
    {
        [SerializeField] Text narrative = default;
        [SerializeField] DialogController controller = default;

        public void Input(DialogNode node)
        {
            narrative.text = node.narrative;
            controller.DisplayOptions();
        }
    }
}

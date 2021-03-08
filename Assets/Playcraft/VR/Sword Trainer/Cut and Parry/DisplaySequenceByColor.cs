using System;
using UnityEngine;

namespace Playcraft.Examples.SwordTrainer
{
    public class DisplaySequenceByColor : MonoBehaviour
    {
        [SerializeField] ColorSO activeColor;
        [SerializeField] ColorSO inactiveColor;
        [SerializeField] ColorSO completeColor;
        [SerializeField] Element[] elements;
    
        public void Input(int value)
        {
            foreach (var element in elements)
            {
                ColorSO status;
                
                if (value == element.index) status = activeColor;
                else if (value > element.index) status = completeColor;
                else status = inactiveColor;
                
                element.color.Input(status);
            }
        }

        [Serializable] public class Element
        {
            public int index;
            public SetColor color;
        }
    }
}

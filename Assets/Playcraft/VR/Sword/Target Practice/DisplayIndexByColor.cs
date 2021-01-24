using System;
using UnityEngine;

namespace Playcraft.Examples.SwordTrainer
{
    public class DisplayIndexByColor : MonoBehaviour
    {
        [SerializeField] ColorSO activeColor;
        [SerializeField] ColorSO inactiveColor;
        [SerializeField] Element[] elements;
    
        public void Input(int value)
        {
            foreach (var element in elements)
            {
                var status = value == element.index ? activeColor : inactiveColor;
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

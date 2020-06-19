using UnityEngine;

namespace Playcraft
{
    public class IntCondition : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] Equality condition;
        [SerializeField] int value;
        [SerializeField] BoolEvent Output;
        #pragma warning restore 0649
    
        public void Input(int query)
        {
            bool result = false;
        
            switch (condition)
            {
                case Equality.Equal: result = query == value; break;
                case Equality.NotEqual: result = query != value; break;
                case Equality.LessThan: result = query < value; break;
                case Equality.GreaterThan: result = query > value; break;
                case Equality.LessThanOrEqual: result = query <= value; break;
                case Equality.GreaterThanOrEqual: result = query >= value; break;
            }
        
            Output.Invoke(result);
        }
    
        enum Equality 
        { 
            Equal, 
            NotEqual, 
            LessThan, 
            GreaterThan, 
            LessThanOrEqual, 
            GreaterThanOrEqual 
        }
    }    
}


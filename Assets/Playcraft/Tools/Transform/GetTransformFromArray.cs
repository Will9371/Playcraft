using UnityEngine;

public class GetTransformFromArray : MonoBehaviour
{
    #pragma warning disable 0649
    [SerializeField] Transform[] transforms;
    [SerializeField] TransformEvent Output;
    [SerializeField] bool allowRepeat;
    #pragma warning restore 0649
    
    int index;
    
    public void Set(int value)
    {
        index = value;
        Output.Invoke(transforms[index]);
    }
    
    public void Cycle(bool reverse)
    {
        index = reverse ? index - 1 : index + 1;
        
        if (index >= transforms.Length)
            index = 0;
        else if (index < 0)
            index = transforms.Length - 1;
        
        Output.Invoke(transforms[index]);
    }
    
    public void Randomize()
    {
        index = RandomStatics.RandomNoRepeat(0, transforms.Length, index);
        Output.Invoke(transforms[index]);
    }
}

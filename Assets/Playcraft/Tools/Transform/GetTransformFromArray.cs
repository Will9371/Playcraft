using UnityEngine;

public class GetTransformFromArray : MonoBehaviour
{
    [SerializeField] Transform[] transforms;
    [SerializeField] TransformEvent Output;
    [SerializeField] bool allowRepeat;
    
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

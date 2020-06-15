using UnityEngine;

public class GetTransformFromArray : MonoBehaviour
{
    [SerializeField] Transform[] transforms;
    [SerializeField] TransformEvent Output;
    [SerializeField] bool allowRepeat;
    
    int index;
    
    public void Cycle()
    {
        index++;
        
        if (index >= transforms.Length)
            index = 0;
        
        Output.Invoke(transforms[index]);
    }
    
    public void Randomize()
    {
        index = RandomStatics.RandomNoRepeat(0, transforms.Length, index);
        Output.Invoke(transforms[index]);
    }
}

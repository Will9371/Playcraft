/*
// PURPOSE: converts between angle and dot product, intended for OnValidate
// INSTRUCTIONS: paste the commented code into your script

[SerializeField] [Range(0f, 360f)] float maxAngle = 45f;
[SerializeField] [Range(-1f, 1f)] float minDot = .5f;

ValidateAngleToDot angleDot;
    
void OnValidate()
{
    if (angleDot == null) angleDot = new ValidateAngleToDot();
    minDot = angleDot.AngleToDot(maxAngle, minDot);
    maxAngle = angleDot.DotToAngle(maxAngle, minDot);
}
*/

namespace Playcraft
{
    public class ValidateAngleToDot
    {   
        float priorMaxAngle;
        float priorMinDot;

        public float AngleToDot(float maxAngle, float minDot)
        {        
            if (priorMaxAngle != maxAngle)
                minDot = VectorMath.AngleToDot(maxAngle);
                
            priorMaxAngle = maxAngle;
            return minDot;
        }
        
        public float DotToAngle(float maxAngle, float minDot)
        {        
            if (priorMinDot != minDot)
                maxAngle = VectorMath.DotToAngle(minDot);
                
            priorMinDot = minDot;
            return maxAngle;
        }
    }
}

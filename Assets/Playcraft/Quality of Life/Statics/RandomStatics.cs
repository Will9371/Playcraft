using UnityEngine;

public static class RandomStatics
{
    public static int RandomNoRepeat(int min, int max, int prior)
    {
        if (max - min < 1)
            return prior;
        
        int result = prior;
        
        while (result == prior)
            result = Random.Range(min, max);
        
        return result;
    }
}

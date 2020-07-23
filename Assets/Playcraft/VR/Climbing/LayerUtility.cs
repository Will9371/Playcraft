using System.Collections.Generic;
using UnityEngine;

// Helper class used that allows you to check for a layer id by the layermask without bit-shifting or relying on strings
namespace Playcraft
{
public class LayerUtility
{
    public bool IsIndexInBinary(int targetIndex, int binary)
    {
        if (binary == -1)
            return true;

        List<bool> maskValues = new List<bool>();

        while (binary >= 1)
        {
            if (binary % 2 == 1)
                maskValues.Add(true);
            else
                maskValues.Add(false);

            binary = Mathf.FloorToInt(binary / 2);
        }

        if (maskValues.Count - 1 < targetIndex)
            return false;
        else
            return maskValues[targetIndex];
    }
}
}

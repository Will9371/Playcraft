using UnityEngine;

public class CursorInterface : MonoBehaviour
{
    public void ShowCursor()
    {
        Cursor.visible = true;
    }
    
    public void HideCursor()
    {
        Cursor.visible = false;
    }
}

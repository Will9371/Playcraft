namespace  Playcraft
{
    public static class MouseStatics
    {
        public static int GetIntFromButton(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.Left: return 0;
                case MouseButton.Right: return 1;
                case MouseButton.Center: return 2;
            }
            
            return -1;
        }
    }    
}


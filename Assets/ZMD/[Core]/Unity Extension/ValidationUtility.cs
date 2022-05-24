// CREDIT: cxode 
// SOURCE: https://forum.unity.com/threads/sendmessage-cannot-be-called-during-awake-checkconsistency-or-onvalidate-can-we-suppress.537265/

using System;

namespace ZMD
{
    /// Sometimes, when you use Unity's built-in OnValidate, it will spam you with a very annoying warning message,
    /// even though nothing has gone wrong. To avoid this, you can run your OnValidate code through this utility.
    public static class ValidationUtility
    {
        /// Call this during OnValidate.
        /// Runs onValidateAction once, after all inspectors have been updated.
        public static void SafeOnValidate(Action onValidateAction)
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.delayCall += _OnValidate;
            
            void _OnValidate()
            {
                UnityEditor.EditorApplication.delayCall -= _OnValidate;
                onValidateAction();
            }
            #endif
        }
    }
}

#region Example Usage
/*
void OnValidate()
{
    ValidationUtility.SafeOnValidate(() =>
    {
        // Put your OnValidate code here
    });
}
*/
#endregion
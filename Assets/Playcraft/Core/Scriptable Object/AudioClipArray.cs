using UnityEngine;

namespace ZMD
{
    [CreateAssetMenu(menuName = "Playcraft/Data Types/Audio Clip Array", fileName = "Audio Clip Set")]
    public class AudioClipArray : ScriptableObject { public AudioClip[] values; }    
}


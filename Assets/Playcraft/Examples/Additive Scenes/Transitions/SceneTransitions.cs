using Playcraft;
using UnityEngine;

public class SceneTransitions : MonoBehaviour
{
    [SerializeField] AnimationProgressEvent fadeIn;
    [SerializeField] AnimationProgressEvent fadeOut;
    
    public void FadeIn() { fadeIn.Play(); }
    public void FadeOut() { fadeOut.Play(); }
}

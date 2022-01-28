using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CurveScreenFade : MonoBehaviour
{
    [Header("SceneReference")]
    public Graphic OverlayTarget;

    [Header("Properties")]
    public AnimationCurve FadeCurve;

    // Total length of fade
    [Min(0.0f)]
    public float FadeTime = 2.5f;
    public Color FadeToColor;

    [Header("Events")]
    public UnityEvent OnFadeIn;
    public UnityEvent OnFadeOut;
    public UnityEvent OnFinished;

    protected float currentScaledTime = 0.0f;
    protected float startTime = 0.0f;
    protected float timeScale = 1.0f;
    protected bool isAnimating = false;
    protected bool playForward = true;

    public void FadeIn()
    {
        Debug.Log("FadeIn");
        StartFade(-timeScale * FadeTime);
    }

    public void FadeOut()
    {
        StartFade(timeScale * FadeTime);
    }

    protected void StartFade(float newTimeScale)
    {
        if (OverlayTarget == null)
        {
            Debug.LogError("FadeScene component must have a valid overlay target!");
            return;
        }

        if (newTimeScale == 0)
            return;

        playForward = newTimeScale > 0;
        currentScaledTime = playForward ? 0.0f : Mathf.Abs(FadeTime);
        startTime = Time.realtimeSinceStartup;
        isAnimating = true;
    }

    void Update()
    {
        if (isAnimating)
        {
            currentScaledTime = (Time.realtimeSinceStartup - startTime)/Mathf.Abs(FadeTime);
            float newAlpha = FadeCurve.Evaluate(currentScaledTime)/FadeCurve.length;
            Color newColor = FadeToColor;
            newColor.a = newAlpha;
            OverlayTarget.color = newColor;

            if (currentScaledTime >= FadeTime)
            {
                isAnimating = false;
                OnFinished.Invoke();

                if (playForward)
                    OnFadeIn.Invoke();
                else
                    OnFadeOut.Invoke();
            }
        }
    }
}
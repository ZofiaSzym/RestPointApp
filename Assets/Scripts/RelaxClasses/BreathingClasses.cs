using System.Collections;
using TMPro;
using UnityEngine;

public class BreathingClasses : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI breathingText;
    [SerializeField] private TextMeshProUGUI counterText;
    private bool isBreathing;

    public void Breath478()
    {
        StartCoroutine(Breath478Routine());
    }

    public void Breath4444()
    {
        StartCoroutine(Breath4444Routine());
    }

    private IEnumerator Breath478Routine()
    {
        if (isBreathing)
            yield break;
        isBreathing = true;
        for (var i = 0; i < 10; i++)
        {
            yield return StartCoroutine(BreathingStep(BreathingState.Inhale, 4));
            yield return StartCoroutine(BreathingStep(BreathingState.Hold, 7));
            yield return StartCoroutine(BreathingStep(BreathingState.Exhale, 8));
        }
    }

    private IEnumerator Breath4444Routine()
    {
        if (isBreathing)
            yield break;
        isBreathing = true;
        for (var i = 0; i < 10; i++)
        {
            yield return StartCoroutine(BreathingStep(BreathingState.Inhale, 4));
            yield return StartCoroutine(BreathingStep(BreathingState.Hold, 4));
            yield return StartCoroutine(BreathingStep(BreathingState.Exhale, 4));
            yield return StartCoroutine(BreathingStep(BreathingState.Hold, 4));
        }
    }

    private IEnumerator BreathingStep(BreathingState state, int duration)
    {
        breathingText.text = state.ToString();
        var time = duration;
        while (time > 0)
        {
            counterText.text = time.ToString();
            time--;
            yield return new WaitForSeconds(1f); // ✅ waits without blocking
        }
    }
}

internal enum BreathingState
{
    Inhale,
    Hold,
    Exhale
}
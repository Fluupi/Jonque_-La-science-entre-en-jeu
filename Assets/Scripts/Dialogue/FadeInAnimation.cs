using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class FadeInAnimation : MonoBehaviour
{
    public UnityEvent endAnimEvent;
    float animIndex;
    public AnimationCurve fadeAnimCurve;
    public float speed;
    public float startAlpha, endAlpha;
    public Image ItemS;
    private void OnEnable()
    {
        StartCoroutine(fadeIn(startAlpha, endAlpha, false));
    }

    public void startFadeIn(float startA, float endA, bool invoke)
    {
        StartCoroutine(fadeIn(startA, endA, invoke));
    }

    IEnumerator fadeIn(float startA, float endA, bool invoke)
    {

        animIndex += Time.deltaTime * speed;

        ItemS.color = new Color(ItemS.color.r, ItemS.color.g, ItemS.color.b, Mathf.Lerp(startA, endA, fadeAnimCurve.Evaluate(animIndex)));

        if (animIndex >= 1)
        {
            ItemS.color = new Color(ItemS.color.r, ItemS.color.g, ItemS.color.b, endA);
            animIndex = 0;
            if(invoke)
                endAnimEvent?.Invoke();
        }
        else
        {
            yield return new WaitForEndOfFrame();
            StartCoroutine(fadeIn(startA, endA, invoke));
        }
    }
}

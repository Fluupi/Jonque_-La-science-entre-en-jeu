using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PopInAnimation : MonoBehaviour
{
    float animIndex;
    public AnimationCurve popAnimCurve;
    public UnityEvent endAnimation;
    public float speed;
    public Vector2 startPosi, endPosi;
    public RectTransform ItemT;
    private void OnEnable()
    {
        StartCoroutine(popIn(startPosi, endPosi, false));
    }

    public void startPopIn(Vector2 startPos, Vector2 endPos, bool invoke)
    {
        StartCoroutine(popIn(startPos, endPos, invoke));
    }

    IEnumerator popIn(Vector2 startPos, Vector2 endPos, bool invoke)
    {

        animIndex += Time.deltaTime * speed;

        ItemT.anchoredPosition = Vector2.Lerp(startPos, endPos, popAnimCurve.Evaluate(animIndex));

        if(animIndex >= 1)
        {
            ItemT.anchoredPosition = endPos;
            animIndex = 0;

            if (invoke)
                endAnimation?.Invoke();
        }
        else
        {
            yield return new WaitForEndOfFrame();
            StartCoroutine(popIn(startPos, endPos, invoke));
        }
    }

}

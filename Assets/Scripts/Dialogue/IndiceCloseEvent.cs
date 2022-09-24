using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class IndiceCloseEvent : MonoBehaviour
{
    public UnityEvent<float, float, bool> IndiceActivatedFade;
    public UnityEvent<Vector2, Vector2, bool> IndiceActivatedPop;

    public void EventCall()
    {
        IndiceActivatedFade?.Invoke(.6f, 0f, false);
        IndiceActivatedPop?.Invoke(new Vector2(0, 230), new Vector2(0, -900), true);
    }
}

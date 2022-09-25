using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tuto : MonoBehaviour
{
    public UnityEvent endTutoEvent;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            endTutoEvent?.Invoke();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEnterEventHandler : MonoBehaviour
{
    public UnityEvent OnTriggerEnterEvent; 
    public UnityEvent OnTriggerExitEvent; 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "player")
        {
            OnTriggerEnterEvent?.Invoke();
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "player")
        {
            OnTriggerExitEvent?.Invoke();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public void FMODSound(string eventName)
    {
        FMODUnity.RuntimeManager.PlayOneShot(eventName);
    }
}

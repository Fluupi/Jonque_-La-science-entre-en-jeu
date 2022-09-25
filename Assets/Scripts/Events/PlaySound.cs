using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public void FMODSound(string eventName)
    {
        print(1);
        FMODUnity.RuntimeManager.PlayOneShot(eventName);
    }
}

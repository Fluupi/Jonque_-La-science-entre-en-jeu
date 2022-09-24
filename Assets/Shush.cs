using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shush : MonoBehaviour
{
    [SerializeField] private GameObject go;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            go.SetActive(false);
    }
}

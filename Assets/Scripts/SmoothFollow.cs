using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public Transform playerT;
    public float smooth;
    private Vector3 velocity = Vector3.zero;
    void Update()
    {

        transform.position = Vector3.SmoothDamp(new Vector3(transform.position.x, transform.position.y, -3), new Vector3(playerT.position.x, playerT.position.y, -3), ref velocity, smooth);
    }   
}

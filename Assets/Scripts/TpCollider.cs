using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpCollider : MonoBehaviour
{
    public Transform otherCollider;
    public bool vertical;
    [HideInInspector] public float symetricPos;
    public float offSet;
    private void Start()
    {
        if (vertical)
        {
            symetricPos = otherCollider.position.y;
        }
        else
        {
            symetricPos = otherCollider.position.x;
        }
    }
}

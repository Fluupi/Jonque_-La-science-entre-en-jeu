using System;
using UnityEngine;

public class Bateau : MonoBehaviour
{
    //Components
    private Rigidbody2D rb;

    //Kick
    [Header("Kick")]
    [SerializeField] [Tooltip("A définir")] private float kickSpeed;
    [SerializeField] private float kickDuration;
    private float kickLeftTime;
    private float kickPercentage;

    //Movement
    [Header("Speed")]
    [SerializeField] [Tooltip("A définir")] private float maxRotationSpeed;
    private bool movingStraight;
    private float speed = 0f;

    //Target
    [SerializeField] private Vector3 target;
    private float maxDistanceFromTarget = 0.05f;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Kick()
    {
        kickLeftTime = kickDuration;
        speed = kickSpeed;
    }

    private void Update()
    {
        //update target pos
        if(!movingStraight)
            target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z * -1));

        //stop boat if near enough
        if(Vector3.Distance(transform.position, target) <= maxDistanceFromTarget)
        {
            kickLeftTime = 0f;
        }

        //update kick%
        float time = Time.deltaTime;
        kickLeftTime -= time;
        kickPercentage = kickLeftTime / kickDuration;

        //update speed according to kick%
        if (kickPercentage > 0f)
        {
            speed = kickSpeed * kickPercentage;
        }
        else if (kickPercentage < 0f)
        {
            kickLeftTime = 0f;
            speed = 0f;
        }
    }

    public void ResetMovingStraight()
    {
        movingStraight = false;
    }

    public void StraightMove(int dir)
    {
        Vector3[] directions = { Vector3.up, Vector3.left, Vector3.down, Vector3.right };

        movingStraight = true;
        target = transform.position + directions[dir] * kickSpeed * kickDuration / 10;
        Kick();
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.up * speed;
        LookTowards();
    }

    private void LookTowards()
    {
        //Compute rotation needed to reach target
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, target - transform.position);   
        
        //Nerf rotation using maxRotationSpeed (how much if max kick) and kick (energy left from last speedup)
        rotation = Quaternion.RotateTowards(transform.rotation, rotation, maxRotationSpeed * kickPercentage);

        //Apply nerfed rotation
        rb.SetRotation(rotation);                                                                              
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(target, .2f);
    }
}
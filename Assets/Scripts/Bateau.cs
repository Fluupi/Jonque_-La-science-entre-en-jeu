using System;
using UnityEngine;

public class Bateau : MonoBehaviour
{
    public bool enDialogue;

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

    [Obsolete("moving straight is not used anymore")]
    private bool movingStraight;
    [SerializeField] private float speed = 0f;

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
        if (enDialogue)
        {
            kickLeftTime = 0f;
            speed = 0f;
            return;
        }

        //update target pos
        target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z * -1));

        //stop boat if near enough
        if(Vector3.Distance(transform.position, target) <= maxDistanceFromTarget)
        {
            kickLeftTime = 0f;
        }
    }

    [Obsolete("oving straight is not used anymore")]
    public void ResetMovingStraight()
    {
        movingStraight = false;
    }

    [Obsolete("The method is not used anymore")]
    public void StraightMove(int dir)
    {
        Vector3[] directions = { Vector3.up, Vector3.left, Vector3.down, Vector3.right };

        movingStraight = true;
        target = transform.position + kickDuration * kickSpeed * directions[dir] / 10;
        Kick();
    }

    private Vector2 lastVelocity;

    private void FixedUpdate()
    {
        //update kick%
        float time = Time.fixedDeltaTime;
        kickLeftTime -= time;
        kickPercentage = kickLeftTime / kickDuration;

        speed = kickSpeed;

        //update speed according to kick%
        if (kickPercentage > 0f)
        {
            speed *= kickPercentage;
        }
        else if (kickPercentage < 0f)
        {
            kickLeftTime = 0f;
            speed = 0f;
        }

        rb.velocity = transform.up * speed;

        if (speed < 1)
            rb.angularVelocity = 0f;

        if(speed > 0)
            LookTowards();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Ship_Collision");
        transform.Rotate(0, 0, 180);
        rb.velocity = Vector2.Reflect(lastVelocity, collision.contacts[0].normal);
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
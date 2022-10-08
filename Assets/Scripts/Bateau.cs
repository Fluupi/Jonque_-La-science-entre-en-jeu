using System;
using UnityEngine;
using UnityEngine.Events;
public class Bateau : MonoBehaviour
{
    public bool enDialogue;
    public UnityEvent endQuestEvent;
    //Components
    private Rigidbody2D rb;

    public int numberOfQuestsDone;

    //Kick
    [Header("Kick")]
    [SerializeField] [Tooltip("A d�finir")] private float kickSpeed;
    [SerializeField] private float kickDuration;
    private float kickLeftTime;
    private float kickPercentage;

    //Movement
    [Header("Speed")]
    [SerializeField] [Tooltip("A d�finir")] private float maxRotationSpeed;

    [Obsolete("moving straight is not used anymore")]
    private bool movingStraight;
    [SerializeField] private float speed = 0f;

    //Target
    [SerializeField] public Vector3 target;
    private float maxDistanceFromTarget = 0.05f;
    TrailRenderer trail;
    GameManager gameManager;
    private void Start()
    {
        trail = GetComponentInChildren<TrailRenderer>();
        rb = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
    }

    public void SwitchEnDialogue(bool inOrOut)
    {
        enDialogue = inOrOut;
    }

    public void Kick()
    {
        kickLeftTime = kickDuration;
        speed = kickSpeed;
    }
    public float tpTimer = 0;
    private void Update()
    {
        tpTimer += Time.deltaTime;

        if (tpTimer < 1)
        {
            trail.time = 0;
        }
        else trail.time = 1;

        if(numberOfQuestsDone >= 3)
        {
            endQuestEvent?.Invoke();
        }

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
        float rd = UnityEngine.Random.Range(0f,1f);
        if(rd > gameManager.probaComCollision && gameManager.CommentaryTimer > gameManager.timeBeforeNewCommentary)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Commentary/Nelson/Collision");
            gameManager.CommentaryTimer = 0;
        }
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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "TpBound" && tpTimer > .1f)
        {
            TpCollider tpCol = collision.GetComponent<TpCollider>();
            print(1);
            var col = tpCol.otherCollider.position;
            if (tpCol.vertical)
            {
                col.x = transform.position.x;
                col.y = tpCol.symetricPos - tpCol.offSet;
            }
            else
            {
                col.x = tpCol.symetricPos - tpCol.offSet;
                col.y = transform.position.y;
            }
            transform.position = col;
            tpTimer = 0;
        }
    }

}
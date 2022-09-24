using UnityEngine;

public class Bateau : MonoBehaviour
{
    //Components
    private Rigidbody2D _rb;

    //Kick
    [Header("Kick")]
    [SerializeField] [Tooltip("A définir")] private float _kickSpeed = 10f;
    [SerializeField] private float _kickDuration;
    private float _kickLeftTime;
    private float _kickPercentage;

    //Movement
    [Header("Speed")]
    [SerializeField] [Tooltip("A définir")] private float _maxRotationSpeed;
    private float speed = 0f;

    //Target
    private Vector3 target;
    private float maxDistanceFromTarget = 0.05f;


    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //update target pos
        target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z * -1));

        //stop boat if near enough
        if(Vector3.Distance(transform.position, target) <= maxDistanceFromTarget)
        {
            _kickLeftTime = 0f;
        }

        //update kick%
        float time = Time.deltaTime;
        _kickLeftTime -= time;
        _kickPercentage = _kickLeftTime / _kickDuration;

        //update speed according to kick%
        if (_kickPercentage > 0f)
        {
            speed = _kickSpeed * time * 100 * _kickPercentage;
        }
        else if (_kickPercentage < 0f)
        {
            _kickLeftTime = 0f;
            speed = 0f;
        }

        //trigger kick boost
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _kickLeftTime = _kickDuration;
            speed = _kickSpeed;
        }
    }

    private void FixedUpdate()
    {
        _rb.velocity = transform.up * speed;
        LookTowards();
    }

    private void LookTowards()
    {
        //Compute rotation needed to reach target
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, target - transform.position);   
        
        //Nerf rotation using maxRotationSpeed (how much if max kick) and kick (energy left from last speedup)
        rotation = Quaternion.RotateTowards(transform.rotation, rotation, _maxRotationSpeed * _kickPercentage);

        //Apply nerfed rotation
        _rb.SetRotation(rotation);                                                                              
    }
}
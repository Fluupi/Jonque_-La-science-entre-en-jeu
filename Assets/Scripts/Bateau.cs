using UnityEngine;

public class Bateau : MonoBehaviour
{
    private Rigidbody2D _rb;

    [SerializeField] [Tooltip("A définir")] private float _kickSpeed = 10f;
    [SerializeField] private float _kickDuration;
    [SerializeField] private float _kickLeftTime;

    [SerializeField] private float speed = 0f;
    private Vector3 mousePosition;
    private Vector3 target;

    [SerializeField] [Tooltip("A définir")] private float _rotationSpeed;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z * -1));

        //Debug.Log("input " + Input.mousePosition + ", après transfo " + target);

        float time = Time.deltaTime;
        _kickLeftTime -= time;

        if (_kickLeftTime > 0f)
        {
            speed = _kickSpeed * time * 100 * (_kickLeftTime / _kickDuration);
        }
        else if (_kickLeftTime < 0f)
        {
            _kickLeftTime = 0f;
            speed = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _kickLeftTime = _kickDuration;
            speed = _kickSpeed;
        }
    }

    private void FixedUpdate()
    {
        _rb.velocity = transform.up * speed;

        Quaternion quat = Quaternion.LookRotation(Vector3.forward, target - transform.position);

        //quat.

        _rb.MoveRotation(quat);

        //LookTowards();
        //_rb.Quaternion.Angle()
        //_rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, rotation, _rotationSpeed));
    }

    private void LookTowards()
    {
        Debug.Log(Vector3.Angle(Vector3.up,target));

        float accuracy = 5f;
        float currentAngle = Vector3.Angle(transform.up, target - transform.position);

        float wantedAngle = Vector3.Angle(Vector3.up, target - transform.position);

        if(Mathf.Abs(currentAngle-wantedAngle) >= accuracy)
        {
            float plus, minus;

            plus = currentAngle + _rotationSpeed;
            minus = currentAngle - _rotationSpeed;

            _rb.rotation = Mathf.Min(Mathf.Abs(plus-wantedAngle), Mathf.Abs(minus-wantedAngle));
        }
    }
}
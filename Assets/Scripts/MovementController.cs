using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float speed;
    public float viewAngle;
    public float sensitivity;

    public GameObject cam;

    private Vector2 currentRotation;
    private bool doRotate = false;

    // Start is called before the first frame update
    void Start()
    {
        // Initiate any setup logic if needed
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (doRotate)
        {
            Rotate();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnsetLockState();
        }
    }

    public void Move()
    {
        transform.position += transform.forward * speed * Time.deltaTime * Input.GetAxis("Vertical");
        transform.position += transform.right * speed * Time.deltaTime * Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.Space)) transform.position += new Vector3(0, speed * Time.deltaTime, 0);
        if (Input.GetKey(KeyCode.LeftShift)) transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
    }

    // https://discussions.unity.com/t/free-mouse-rotating-camera/186279/3
    public void Rotate()
    {
        currentRotation.x += Input.GetAxis("Mouse X") * sensitivity;
        currentRotation.y -= Input.GetAxis("Mouse Y") * sensitivity;
        currentRotation.x = Mathf.Repeat(currentRotation.x, 360);
        currentRotation.y = Mathf.Clamp(currentRotation.y, -viewAngle, viewAngle);
        transform.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);
    }

    public void SetLockState()
    {
        Cursor.lockState = CursorLockMode.Locked;
        doRotate = true;
    }

    public void UnsetLockState()
    {
        Cursor.lockState = CursorLockMode.None;
        doRotate = false;
    }
}

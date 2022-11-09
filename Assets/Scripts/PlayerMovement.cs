using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float crouchSpeed;
    public float speed;
    public float sprintSpeed;
    public Transform orientation;
    public float playerHeight;
    public LayerMask groundcheck;
    public float jumpheight;
    public float maxSlopeAngle;

    private float horizontalInput;
    private float verticalInput;
    private Vector3 direction;
    private Rigidbody rb;
    private bool isGrounded;
    private RaycastHit slopeHit;

    [SerializeField]private float playerRaycastOffset = 0.3f;
    [SerializeField] private float normalSpeed=10f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    // Update is called once per frame
    void Update()
    {
        MyInput();

        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.2f,groundcheck);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            
            rb.AddForce(Vector3.up * jumpheight, ForceMode.Impulse);
        }
        ChangeSpeed();
        MovePlayer();

    }
    private bool OnSlope()
    {
        float playerCenter = playerHeight * 0.5f;
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerCenter+playerRaycastOffset))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }
        return false;
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }
    private void MovePlayer()
    {
        direction = (orientation.forward * verticalInput  + orientation.right * horizontalInput).normalized ;
        if (OnSlope())
        {
            rb.velocity = SlopeDirection()*speed ;
        }
        else
        {
            rb.velocity = direction*speed ;
        }
    }
    private Vector3 SlopeDirection()
    {
        return Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized;
    }
    private void ChangeSpeed()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = sprintSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = normalSpeed;
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            transform.localScale = new Vector3(transform.localScale.x, 0.5f, transform.localScale.z);
            speed = crouchSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            transform.localScale = new Vector3(transform.localScale.x, 1f, transform.localScale.z);
            speed = normalSpeed;

        }
    }

}

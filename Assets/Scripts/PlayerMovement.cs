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
    float horizontalInput;
    float verticalInput;
    Vector3 direction;
    Rigidbody rb;
    public float playerHeight;
    public LayerMask groundcheck;
    bool isGrounded;
    public float jumpheight;
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private bool OnSLope()
    {
        if(Physics.Raycast(transform.position,Vector3.down,out slopeHit, playerHeight *0.5f+ 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }
        return false;
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
        MovePlayer();
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = sprintSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 10f;
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            transform.localScale = new Vector3(transform.localScale.x, 0.5f, transform.localScale.z);
            speed = crouchSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            transform.localScale = new Vector3(transform.localScale.x, 1f, transform.localScale.z);
            speed = 10f;
            
        }
    }
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }
    private void MovePlayer()
    {
        direction = orientation.forward * verticalInput * speed + orientation.right * horizontalInput * speed ;
        rb.velocity = direction;
        if (OnSLope())
        {
            rb.velocity = SlopeDirection()*speed ;
        }
    }
    private Vector3 SlopeDirection()
    {
        return Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized;
    }

}

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
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        direction = orientation.forward * verticalInput * speed + orientation.right * horizontalInput * speed + transform.up * rb.velocity.y;
        rb.velocity = direction;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRunning : MonoBehaviour
{

    [SerializeField] Transform orientation;

    [Header("WallRunning")]
    [SerializeField] float wallDistance = .5f;
    [SerializeField] float minJumpHeight = 1f;
    [SerializeField] private float wallRunGravity;
    [SerializeField] private float WallJumpForce;

    bool WallLeft = false;
    bool WallRight = false;
    RaycastHit leftWallHit;
    RaycastHit rightWallHit;
    PlayerMovement pm;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    bool CanWallRun()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minJumpHeight);
    }

    void CheckforWall()
    {
        WallLeft = Physics.Raycast(transform.position, -orientation.right,out leftWallHit, wallDistance);
        WallRight = Physics.Raycast(transform.position, orientation.right,out rightWallHit, wallDistance);

    }
    private void Update()
    {
        CheckforWall();

        if (CanWallRun())
        {
            if (WallLeft || WallRight)
            {
                StartWallRun();
            }

        }
    }

    void StartWallRun()
    {
        rb.useGravity = false;

        if (Input.GetKey(KeyCode.Space))
        {
            if (WallLeft)
            {
                rb.AddForce(leftWallHit.normal * wallRunGravity, ForceMode.Force);
               // rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            }
            else if (WallRight)
            {
                rb.AddForce(rightWallHit.normal * wallRunGravity, ForceMode.Force);
                //rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            if (WallLeft)
            {
                rb.AddForce(leftWallHit.normal * WallJumpForce, ForceMode.Impulse);
            }
            else if (WallRight)
            {
                rb.AddForce(rightWallHit.normal*WallJumpForce,ForceMode.Impulse);  
            }
        }
        else
        {
            StopWallRun();
        }
    }
    void StopWallRun()
    {
        rb.useGravity = true;
    }
}

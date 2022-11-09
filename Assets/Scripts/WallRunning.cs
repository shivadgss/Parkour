using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRunning : MonoBehaviour
{

    [SerializeField] Transform orientation;

    [Header("WallRunning")]
    [SerializeField] float wallDistance = .5f;
    [SerializeField] float minJumpHeight = 1f;
    private Rigidbody rb;
    [SerializeField] private float wallRunGravity;
    [SerializeField] private float WallJumpForce;

    bool WallLeft = false;
    bool WallRight = false;
    RaycastHit leftWallHit;
    RaycastHit rightWallHit;
    PlayerMovement pm;

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
            if (WallLeft)
            {
                StartWallRun();
            }
            else if (WallRight)
            {
                StartWallRun();
            }
            else
            {
                StopWallRun();
            }
        }
    }

    void StartWallRun()
    {
        rb.useGravity = false;
        rb.AddForce(Vector3.down * wallRunGravity, ForceMode.Force);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (WallLeft)
            {
                Vector3 wallRunJumpDirection = transform.up +leftWallHit.normal;
                rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
                rb.AddForce(wallRunJumpDirection*WallJumpForce*100f, ForceMode.Force);
            }
            else if (WallRight)
            {
                Vector3 wallRunJumpDirection = transform.up + rightWallHit.normal;
                rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
                rb.AddForce(wallRunJumpDirection * WallJumpForce * 100f, ForceMode.Force);
            }
        }
    }
    void StopWallRun()
    {
        rb.useGravity = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform camera;

    public float playerMovementSpeed = 6f;

    public float turnSmoothTime = 0.1f;

    float turnSmoothVelocity;

    [Header("Jump Fields")]
    public LayerMask whatIsGround;
    bool IsGrounded = true;
    //Anything higher than 500 will make the player jump above the house
    [Range(100, 500)]
    public int jumpForce;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        //Reading player input
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        //checks for movement on any direction
        if (direction.magnitude >= 0.1f)
        {
            //Makes the character turn in the direction the camera is facing
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            //Moves the character based on what position the camera is facing
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            transform.Translate(moveDirection.normalized * playerMovementSpeed * Time.deltaTime, Space.World);
          //  controller.Move(moveDirection.normalized * playerMovementSpeed * Time.deltaTime);

        }

        if (Input.GetButtonDown("Jump") && IsGrounded)
        {
            //Makes the player jump and then sets the IsGrounded check to false so you cannot jump midair
            StartCoroutine("Jump");
            IsGrounded = false;
        }
    }

    IEnumerator Jump()
    {
        //Adds force to the rigidbody which can be increases by changing jumpForce in the editor
        this.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce);
        yield return null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Skips the check if they are not grounded
        if(!IsGrounded)
        {
            //If the player is touching what we consider ground then we turn the boolean to true to allow a jump
            if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                IsGrounded = true;
            }
        }
    }
}

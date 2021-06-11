using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Access player controller")]
    public CharacterController playerController;
    public float inputX;
    public float inputY;
    [Header("Player movement ability attributes")]
    public float speed = 12;
    [Space]
    public float jumpHeight;
    [Space]
    public bool isGround;
    [Space]
    Vector3 velocity;


    //public float gravity = -9.81f;
    //[Space]
    //public Transform groundCheckPos;
    //public float groundCheckRad;
    //public LayerMask groundLayer;

    void Start()
    {
        if (playerController == null)
        {
            playerController = this.gameObject.GetComponentInChildren<CharacterController>();
        }
    }

    void Update()
    {
        Movement();
        //GroundCheck();
        //FallOnGroundPhysx();
        //Jump();
    }
    #region Movement Ability Functions
    public void Movement()
    {
        inputX = Input.GetAxis("Horizontal"); //Left and Right Axis
        inputY = Input.GetAxis("Vertical"); //Forward and Backwards axis
        Vector3 move = transform.right * inputX + transform.forward * inputY; //Every frame Vector3 (right * input + Forward * input)
        playerController.Move(move * speed * Time.deltaTime); //Move with shit
    }

    //public void GroundCheck()
    //{
    //    isGround = Physics.CheckSphere(groundCheckPos.position, groundCheckRad, groundLayer);
    //}

    //public void Jump()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space) && isGround)
    //    {
    //        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    //    }
    //}

    //public void FallOnGroundPhysx()
    //{
    //    velocity.y += gravity * Time.deltaTime;
    //    playerController.Move(velocity * Time.deltaTime);
    //    if (isGround && velocity.y < 0)
    //    {
    //        velocity.y = -2f;
    //    }
    //}
    #endregion

    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(groundCheckPos.position, groundCheckRad);
    //}
}
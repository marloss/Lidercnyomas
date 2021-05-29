using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Access player controller")]
    public CharacterController playerController;
    [SerializeField]float inputX;
    [SerializeField] float inputY;
    [Header("Player movement ability attributes")]
    public float speed = 12;
    [Space]
    public float jumpHeight;
    [Space]
    public bool isGround;
    [Space]
    Vector3 velocity;

    public Image current_Weapon_image;

    public float yPos;
    public float xPos;
    public float view_bobbing_amount;
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
        yPos = current_Weapon_image.rectTransform.position.y;
        xPos = current_Weapon_image.rectTransform.position.x;
    }

    void Update()
    {
        Movement();
        //GroundCheck();
        //FallOnGroundPhysx();
        //Jump();
        Vector3 rect = new Vector3(xPos + (Mathf.Sin(-inputX) * view_bobbing_amount), yPos + (Mathf.Sin(-inputY) * view_bobbing_amount));
        current_Weapon_image.rectTransform.position = rect;

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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    private bool isFacingRight = true;
    private bool isWalking;

    private bool isGrounded;
    private bool canjump;
   

    private float movementInputDirection;

    public float movementSpeed = 10.0f;
    public float jumpForce = 16.0f;
    public float groundCheckRadius;
   
    private int amountOfJumpsLeft;

    public int amountOfJumps = 1;
    

    public LayerMask whatIsGround;

    public Transform groundCheck;
    
    private Rigidbody2D rb;

    public Animator playerAnim;

    public int thisPlayer;
    string playerInputAxis;
    string JumpInputAxis;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        amountOfJumpsLeft = amountOfJumps;
        if (CompareTag("Player1"))
            thisPlayer = 1;
        else
            thisPlayer = 2;
        
        if (thisPlayer == 1)
        {
            playerInputAxis = "Horizontal";
            JumpInputAxis = "Jump";            
        }
        else
        {
            playerInputAxis = "HorizontalP2";
            JumpInputAxis = "Jump2";
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        checkMovementDirection();
        UpdateAnimations();
        CheckSurroundings();
        checkIfCanJump();        

    }
    private void FixedUpdate()
    {
        Applymovement();
    }

    private void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        
    }
    private void checkIfCanJump()
    {
        if(isGrounded && rb.velocity.y <= 0)
        {
            amountOfJumpsLeft = amountOfJumps;
        }

        if(amountOfJumpsLeft <= 0)
        {
            canjump = false;
        }
        else
        {
            canjump = true;
        }
    }   
    private void checkMovementDirection()
    {
        if(isFacingRight && movementInputDirection < 0)
        {
            Flip();          
        }
        else if(!isFacingRight && movementInputDirection > 0)
        {
            Flip();            
        }

        /*
        if(rb.velocity.x != 0)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
        */
    }
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0, 180, 0f, 0);
    }
    private void UpdateAnimations()
    {
        //playerAnim.SetBool("isWalking", isWalking);
        //playerAnim.SetBool("isGrounded", isGrounded);
    }
    private void CheckInput()
    {
       
        movementInputDirection = Input.GetAxisRaw(playerInputAxis);

        if(Input.GetButtonDown(JumpInputAxis))
        {
            Jump();
        }
    }
    private void Applymovement()
    {
        rb.velocity = new Vector2(movementInputDirection * movementSpeed, rb.velocity.y);
    }
    private void Jump()
    {
        if (canjump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            amountOfJumpsLeft--;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        
        
    }

}

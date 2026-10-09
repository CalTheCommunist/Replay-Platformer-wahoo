using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovomente : MonoBehaviour
{
    public Transform cam;
    public Rigidbody player;

    public InputActionReference move;
    
    public float speed;
    private Vector2 direction;
    

    public InputActionReference jumping;
    public float jumpHeight = 2f;
    private bool jumpPressed = false;
    public bool isGrounded = true;
   
    public Animator animator;
    


    private void Jump(InputAction.CallbackContext obj)
    {
        jumpPressed = true;
    }


   
    void Start()
    {
        //lock the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //get the jump function
        jumping.action.started += Jump;
        //get the animator
        animator = GetComponent<Animator>();
    }

    
    void Update()
    {
        //read value from player input
        direction = move.action.ReadValue<Vector2>();
        //keep the speed updated in the animator for the animations
        animator.SetFloat("Speed",player.linearVelocity.magnitude);
      

    }
   
    private void FixedUpdate()
    {
      //call jump action if the player presses space
        if (jumpPressed && isGrounded)
        {
            JumpAction();
        }
        //checks to see if you're falling so it plays the falling animation if you fall without jumping
        else if(isGrounded == false && jumpPressed == true)
        {
            jumpPressed = false;
            animator.SetBool("jumping", jumpPressed);
            
        }
        //temporary movement until i find a better way to do this, originally used to turn the player where you were walking instead of just sliding to the side.
        Vector3 camDir = cam.transform.rotation * direction;
        Vector3 targetDirection = new Vector3(camDir.x, 0, camDir.z);
      
        transform.rotation = new quaternion(transform.rotation.x, cam.rotation.y, transform.rotation.z, transform.rotation.w);
        player.linearVelocity = new Vector3(
     targetDirection.normalized.x * speed,
     player.linearVelocity.y,
     targetDirection.normalized.z * speed);

    }
    private void OnCollisionStay(Collision collision)
    {
        //checks to see if you're on the ground
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                isGrounded = true;
                animator.SetBool("OnGround", isGrounded);
                break;
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        //this lets the animator know when you're off the ground.
        isGrounded = false;
        animator.SetBool("OnGround", isGrounded);
    }
    /// <summary>
    /// this function calculates the jump 
    /// and lets the script and animator know when you're in the air
    /// </summary>
    private void JumpAction()
    {
            player.linearVelocity = new Vector3(
                player.linearVelocity.x,
                Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y),
                player.linearVelocity.z
            );

            isGrounded = false;
            jumpPressed = false;
            animator.SetBool("jumping", jumpPressed);
            animator.SetBool("OnGround", isGrounded);

    }
    
}

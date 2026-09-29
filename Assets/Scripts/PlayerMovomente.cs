using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovomente : MonoBehaviour
{
    public Transform cam;
    public Rigidbody player;

    public InputActionReference move;
    public float turnsmoothtime = 0.4f;
    public float speed;
    private Vector2 direction;
    private float turnvelocity = 5f;

    public InputActionReference jumping;
    public float jumpHeight = 2f;
    private bool jumpPressed = false;
    public bool isGrounded = true;
   
    public Animator animator;
    


    private void Jump(InputAction.CallbackContext obj)
    {
        jumpPressed = true;
    }


    private void OnEnable()
    {
        jumping.action.started += Jump;
       
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        animator = GetComponent<Animator>();
    }

    
    void Update()
    {
        direction = move.action.ReadValue<Vector2>();

        animator.SetFloat("Speed",player.linearVelocity.magnitude);
      

    }
   
    private void FixedUpdate()
    {
      
        if (jumpPressed && isGrounded)
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
        if(isGrounded == false && jumpPressed == true)
        {
            jumpPressed = false;
            animator.SetBool("jumping", jumpPressed);
            
        }

        Vector3 camDir = cam.transform.rotation * direction;
        Vector3 targetDirection = new Vector3(camDir.x, 0, camDir.z);
      
        transform.rotation = new quaternion(transform.rotation.x, -cam.rotation.y, transform.rotation.z, transform.rotation.w);
        player.linearVelocity = new Vector3(
     targetDirection.normalized.x * speed,
     player.linearVelocity.y,
     targetDirection.normalized.z * speed);

    }
    private void OnCollisionStay(Collision collision)
    {
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
        isGrounded = false;
        animator.SetBool("OnGround", isGrounded);
    }
    
}

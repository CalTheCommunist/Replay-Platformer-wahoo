using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovomente : MonoBehaviour
{
    public Transform cam;
    public Rigidbody player;
    public InputActionReference jumping;
    public InputActionReference move;

    public float turnsmoothtime = 0.4f;
    public float speed;
    private Vector2 direction;
    private float turnvelocity = 5f;

    

    private void Jump(InputAction.CallbackContext obj)
    {
        Debug.Log("JUMP BITCH!");
    }

    private void OnEnable()
    {
        jumping.action.started += Jump;
    }
    void Start()
    {
       
    }

    
    void Update()
    {
        direction = move.action.ReadValue<Vector2>();
          


    }
   
    private void FixedUpdate()
    {
        Vector3 camDir = cam.transform.rotation * direction;
        Vector3 targetDirection = new Vector3(camDir.x, 0, camDir.z);
        if(direction != Vector2.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetDirection), Time.deltaTime * turnvelocity);
        }
        
        player.linearVelocity = targetDirection.normalized * speed;

    }
}

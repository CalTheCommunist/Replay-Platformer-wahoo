using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovomente : MonoBehaviour
{
    public Rigidbody player;
    public float speed;
    public InputActionReference jumping;
    public InputActionReference move;

    private Vector2 direction;
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
        player.linearVelocity = new Vector3(direction.x * speed, 0, direction.y * speed);
    }
}

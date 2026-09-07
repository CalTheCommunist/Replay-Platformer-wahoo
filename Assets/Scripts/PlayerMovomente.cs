using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovomente : MonoBehaviour
{
    public Rigidbody player;
    public float speed;
    public InputActionReference jumping;

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
        
    }
}

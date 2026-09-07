using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovomente : MonoBehaviour
{
    public Rigidbody player;
    public float speed;
    public InputActionReference jump;
    void Start()
    {
       player = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        
    }
}

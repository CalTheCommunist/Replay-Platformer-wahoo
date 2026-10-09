using UnityEngine;

public class rotationscript : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 3f;
    void Start()
    {
        
    }

    void Update()
    {
        // rotate around Z axis (degrees per second)
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}

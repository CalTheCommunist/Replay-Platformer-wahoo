using UnityEngine;

public class CameraFix : MonoBehaviour
{
    [SerializeField] private Transform playercam;
    void Start()
    {
        
    }

    
    void Update()
    {
        transform.position = playercam.position;
        transform.rotation = playercam.rotation;
    }
}

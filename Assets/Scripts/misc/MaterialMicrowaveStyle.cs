using UnityEngine;

public class MaterialMicrowaveStyle : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1.2f;
    
    private Material Mat;
    private float time;


    void Start()
    {
       Mat = GetComponent<Renderer>().material;

    }

    void Update()
    {
        time += Time.deltaTime * rotationSpeed;
        Mat.SetFloat("TiltingX", Time.time * rotationSpeed);
    }
}


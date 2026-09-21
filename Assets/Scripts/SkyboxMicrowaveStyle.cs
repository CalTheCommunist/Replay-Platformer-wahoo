using UnityEngine;

public class SkyboxMicrowaveStyle : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1.2f;
    private Color targetColor;
    private Material skyboxMaterial;
    private float time;


    void Start()
    {
        skyboxMaterial = RenderSettings.skybox; // Get the current skybox material
        targetColor = skyboxMaterial.GetColor("_Tint"); // Get the initial color
    }

    void Update()
    {
        time += Time.deltaTime * rotationSpeed;
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotationSpeed);
        Color newColor = Color.Lerp(skyboxMaterial.GetColor("_Tint"), targetColor, time); // Interpolate color

        skyboxMaterial.SetColor("_Tint", newColor); // Set the new color to the skybox

        if (time >= 5.0f)
        {
            time = 0; // Reset time
            targetColor = new Color(Random.value, Random.value, Random.value); // Change target color randomly

        }
    }
}


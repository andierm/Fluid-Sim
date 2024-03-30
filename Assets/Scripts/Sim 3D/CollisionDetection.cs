using UnityEngine;

public class ShaderCollisionDetection : MonoBehaviour
{
    public Shader shader; // Reference to your shader
    public string collisionTag = "Windmill"; // Tag of the objects you want to detect collision with

    private Material material; // Material using the shader

    private void Start()
    {
        material = new Material(shader); // Create a material using your shader
        GetComponent<Renderer>().material = material; // Assign the material to the renderer

        // Check for collision with objects tagged with the specified tag
        bool isColliding = CheckCollision();
        
        // Pass the collision information to the shader
        material.SetInt("_IsColliding", isColliding ? 1 : 0);
    }

    private bool CheckCollision()
    {
        // Perform collision detection here (e.g., using Physics.Raycast)
        // Example:
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider.CompareTag(collisionTag))
            {
                return true; // Collision detected with object of specified tag
            }
        }
        return false; // No collision detected
    }
}

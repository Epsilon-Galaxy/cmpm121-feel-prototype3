using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Vibration Settings")]
    public float maxDistance = 2f; // Maximum distance to start vibrating
    public float minThreshold = 0.25f; // Minimum threshold before vibrating
    public float maxVibrationIntensity = 1.0f; // Maximum vibration intensity
    public float duration = 0.1f; // Duration of the vibration

    [Header("Player Movement Settings")]
    public float playerSpeed = 5.0f; // Speed of the player

    public LayerMask layerMask;

    Rigidbody2D rb;
    private GameObject[] walls;

    void Awake() {
        if (RumbleManager.instance == null)
        {
            Debug.LogWarning("RumbleManager instance is not set.");
        } 
        rb = GetComponent<Rigidbody2D>();

        walls = GameObject.FindGameObjectsWithTag("Wall");
    }

    void Update()
    {
        HandleVibration();
        ToggleWalls();
    }

    void HandleVibration()
    {
        float closestDistance = float.MaxValue;

        // Iterate through all directions to find the closest wall
        for (float angle = 0; angle < 360; angle += 15) 
        {
            Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxDistance, layerMask);
            if (hit.collider != null)
            {
            float distance = hit.distance;
            if (distance < closestDistance)
            {
                closestDistance = distance;
            }

            // Draw the raycast for visualization
            Debug.DrawRay(transform.position, direction * distance, Color.red);
            }
            else
            {
            // Draw the raycast for visualization
            Debug.DrawRay(transform.position, direction * maxDistance, Color.green);
            }
        }

        // Map the closest distance to a vibration intensity (0.0 to 1.0)
        float vibrationIntensity = 1 - Mathf.Clamp01(Mathf.Pow((maxDistance - closestDistance) / maxDistance, 2)) * maxVibrationIntensity;
        if (RumbleManager.instance != null && vibrationIntensity > minThreshold)
        {
            RumbleManager.instance.RumblePulse(vibrationIntensity, vibrationIntensity, duration);
            Debug.Log(vibrationIntensity);
        }
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        if (movement.magnitude > 1)
        {
            movement.Normalize();
        }

        rb.linearVelocity = movement * playerSpeed;
    }

    void ToggleWalls()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            foreach (GameObject wall in walls)
            {
                wall.GetComponent<Renderer>().enabled = false|| !wall.GetComponent<Renderer>().enabled;
            }
        }
    }
}


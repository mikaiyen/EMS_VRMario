using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadTilting : MonoBehaviour
{
    [Header("Player")]
    public GameObject Player;
    public GameObject CenterEyeCamera;

    [Header("Lane Indicators")]
    public Transform[] LaneIndicators; // Array to hold the 3 lane indicators transforms
    public int CurrentLane {get; private set;} = 1; // Start at middle lane

    [Header("Rotation Settings")]
    [Tooltip("Keyboard debug mode")]
    public bool debugMode = false;
    [Range(15f, 45f)]
    [Tooltip("Degree threshold for lane switching")]
    public float rotationThreshold = 30f; 
    [Range(5f, 15f)]
    [Tooltip("Degree threshold to allow next switch")]
    public float resetThreshold = 5f;
    public bool canSwitchLanes = true;
    
    private Quaternion initialRotation;
    
    void Start()
    {
        if (Player == null)
            Player = GameObject.FindWithTag("Player");
            
        // Store initial rotation
        if (CenterEyeCamera != null)
        {
            initialRotation = CenterEyeCamera.transform.rotation;
        }

        // Check if lane indicators are assigned and have the correct number of transforms
        if (LaneIndicators == null || LaneIndicators.Length != 3)
        {
            Debug.LogError("Please assign exactly 5 lane indicators transforms in the inspector.");
        }
    }

    void Update()
    {
        if (CenterEyeCamera == null || Player == null || LaneIndicators == null || LaneIndicators.Length != 3) return;

        // Calculate rotation difference from initial position
        float currentTilt = CenterEyeCamera.transform.rotation.eulerAngles.z;
        float initialTilt = initialRotation.eulerAngles.z;
        float tiltDifference = Mathf.Abs(Mathf.DeltaAngle(currentTilt, initialTilt));

        // Check if head is close to neutral position
        if (tiltDifference < resetThreshold)
        {
            canSwitchLanes = true;
        }
        
        if (debugMode)
        {
            // Debug mode using keyboard controls
            if (Input.GetKeyDown(KeyCode.Z)) // Left
            {
                if (CurrentLane > 0)
                {
                    SwitchLane(-1);
                }
            }
            else if (Input.GetKeyDown(KeyCode.C)) // Right 
            {
                if (CurrentLane < 2)
                {
                    SwitchLane(1);
                }
            }
        }
        else if (canSwitchLanes && tiltDifference > rotationThreshold)
        {
            // Determine direction based on tilt
            if (currentTilt > 180f) // Tilting left
            {
                if (CurrentLane < 2) // Can move right
                {
                    SwitchLane(1);
                }
            }
            else // Tilting right
            {
                if (CurrentLane > 0) // Can move left
                {
                    SwitchLane(-1);
                }
            }
            canSwitchLanes = false; // Prevent continuous switching
        }
    }

    private void SwitchLane(int direction)
    {
        CurrentLane += direction;
        // Set new position based on the corresponding lane indicator
        Vector3 newPosition = Player.transform.position;
        newPosition.x = LaneIndicators[CurrentLane].position.x;
        Player.transform.position = newPosition;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forwarding : MonoBehaviour
{
    // Player
    [Header("Player")]
    public GameObject Player;
    public CharacterController PlayerCH;
    public GameObject RightHand;
    public GameObject LeftHand;

    [Header("Basic Movement Parameter")]
    // public float MoveDuration = 0.001f;
    public float Distance = 3f;
    public enum SpeedMode
    {
        ControllerVelocity,
        HandSpeed
    }
    [Header("Speed Mode")]
    public SpeedMode currentSpeedMode = SpeedMode.ControllerVelocity;
    
    // Speed multiplier constants
    [Header("Controller and Hand Speed Multiplier")]
    [Range(0.1f, 10f)]
    public float controllerVelocityMultiplier = 1.5f; 

    [Range(0.01f, 1f)]
    public float handSpeedMultiplier = 0.15f; 

    private Vector3 rightControllerVelocity;
    private Vector3 leftControllerVelocity;
    private Vector3 lastRightHandPosition;
    private Vector3 lastLeftHandPosition;

    [Header("Lane Reference")]
    public HeadTilting headTilting;


    void Start()
    {
        if (Player == null)
            Player = GameObject.FindWithTag("Player");

    }

    void FixedUpdate()
    {
        // Debug.LogWarning("Controller Velocity: Right Controller ->" + rightControllerVelocity + "; Left Controller -> " + leftControllerVelocity);
        // Debug.LogWarning("Hand Speed: Right hand -> " + string.Format("{0:0.00}", rightHandSpeed) + "; Left hand -> " + string.Format("{0:0.00}", leftHandSpeed));
    }

    public void MoveForward()
    {
        float speedMultiplier = 1f;
        
        if (currentSpeedMode == SpeedMode.ControllerVelocity)
        {
            // Average the magnitude of both controller velocities
            rightControllerVelocity = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch);
            leftControllerVelocity = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch);
            float avgControllerSpeed = (rightControllerVelocity.magnitude + leftControllerVelocity.magnitude) / 2f;
            speedMultiplier = avgControllerSpeed * controllerVelocityMultiplier;
        }
        else // HandSpeed mode
        {
            float rightHandSpeed = Vector3.Distance(lastRightHandPosition, RightHand.transform.position) / Time.deltaTime;
            lastRightHandPosition = RightHand.transform.position;  
            float leftHandSpeed = Vector3.Distance(lastLeftHandPosition, LeftHand.transform.position) / Time.deltaTime;
            lastLeftHandPosition = LeftHand.transform.position; 
            float avgHandSpeed = (rightHandSpeed + leftHandSpeed) / 2f;
            speedMultiplier = avgHandSpeed * handSpeedMultiplier;
        }

        // Clamp the multiplier to prevent extreme values
        speedMultiplier = Mathf.Clamp(speedMultiplier, 1f, 4f);
        // Debug
        Debug.LogWarning("Speed Multiplier: " + speedMultiplier);

        if (headTilting != null && headTilting.LaneIndicators != null && headTilting.LaneIndicators.Length > 0)
        {
            // Get the forward direction of the current lane indicator
            Vector3 laneDirection = headTilting.LaneIndicators[headTilting.CurrentLane].forward;
            laneDirection.y = 0; // Keep movement at the same height
            laneDirection = laneDirection.normalized;
            Debug.LogWarning("Lane Direction: " + laneDirection);
            //Player.transform.position += laneDirection * Distance * speedMultiplier;
            PlayerCH.Move(laneDirection * Distance * speedMultiplier);
        }
    }
}
    
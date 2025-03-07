using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject Player;

    // List to store teleport points
    public List<Transform> teleportPoints = new List<Transform>();
    private int currentTeleportIndex = 0;

    void Start()
    {
        if (Player == null)
            Player = GameObject.FindWithTag("Player");
    }

    public void TeleportToPoint()
    {
        // Check if there are any teleport points registered
        if (teleportPoints.Count > 0)
        {
            // Randomly select a teleport point
            currentTeleportIndex = Random.Range(0, teleportPoints.Count);
            
            // Log the selected teleport point index
            Debug.Log($"Teleporting to point {currentTeleportIndex} at position {teleportPoints[currentTeleportIndex].position}");
            
            // Teleport to the randomly selected point
            Player.transform.position = teleportPoints[currentTeleportIndex].position;
        }
        else
        {
            Debug.LogWarning("No teleport points registered!");
        }
    }

}

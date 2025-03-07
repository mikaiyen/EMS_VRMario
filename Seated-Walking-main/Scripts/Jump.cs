using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public GameObject Player;
    public CharacterController PlayerCH;

    [Range(1f, 100f)]
    public float Force = 10f;
    public float forwardForce = 0.5f, upperForce = 1;
    private Rigidbody rb;
    public bool isGrounded;

    void Start()
    {
        if (Player == null)
            Player = GameObject.FindWithTag("Player");

        rb = Player.GetComponent<Rigidbody>();
        isGrounded = Player.GetComponent<Movement>().isGrounded;
    }

    public void JumpForward()
    {
        if (isGrounded)
        {
            Vector3 direction = Player.transform.forward;
            //rb.AddForce((direction * 0.5f + Vector3.up ) * Force, ForceMode.Impulse);
            Movement.movementValue.y = Mathf.Sqrt(upperForce* -3.0f* Movement.gravity)*Force;
            Movement.movementValue += Player.transform.forward* forwardForce* Force;
            //isGrounded = false;
        }

    }

}

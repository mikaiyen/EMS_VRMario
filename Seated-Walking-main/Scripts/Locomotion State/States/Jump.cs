using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LocomotionStateMachine
{
    public class Jump : LocomotionState
    {
        private Rigidbody _rigidbody;

        // [Range(1f, 100f)]
        // public float Force = 10f;

        public override void StateAction()
        {
            base.StateAction();
            Debug.Log("Jump State");
            // _rigidbody.AddForce(Vector3.up * Force);
        }
    }
}

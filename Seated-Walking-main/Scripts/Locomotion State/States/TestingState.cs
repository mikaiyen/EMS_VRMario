using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LocomotionStateMachine    
{
    public class TestingState : LocomotionState
    {
        public GameObject gameObject;

        public override void StateAction()
        {
            base.StateAction();
            Debug.Log("Testing State");
            if (gameObject != null)
            {
                gameObject.SetActive(false);
            }
        }
    }
}

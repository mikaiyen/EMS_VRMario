using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LocomotionStateMachine
{
    public class PreTeleport : LocomotionState
    {
        public override void StateAction()
        {
            base.StateAction();
            Debug.Log("PreTeleport State");
        }
    }   
}


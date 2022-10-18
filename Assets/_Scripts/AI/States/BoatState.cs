using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatState : AIStates
{
    bool inBoat;
    public override AIStates RunCurrentState()
    {
        if(inBoat)
        {
            animator.SetBool("Run", false);
            animator.SetBool("Idle", true);

            // dont change state
            return this;
        }
        aIStateManager.SwitchToCollectState();
        return aIStateManager.CurrentState;
    }
}

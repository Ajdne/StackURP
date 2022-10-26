using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState : AIStates
{
    private bool animationOver;
    public bool AnimationOver { get { return animationOver; } set { animationOver = value; } }

    public override AIStates RunCurrentState()
    {
        if(animationOver)
        {
            GetComponent<IMovement>().ActivateMovement();

            aIStateManager.SwitchToCollectState();
            return aIStateManager.CurrentState;
        }

        return this;
    }
}

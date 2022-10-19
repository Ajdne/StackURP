using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BoatState : AIStates
{
    bool inBoat;

    public bool InBoat { get => inBoat; set => inBoat = value; }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    public override AIStates RunCurrentState()
    {    
        //turn of nav mesh to jump 
        agent.enabled = false;

        if (InBoat)
        {
            //animator.SetBool("Run", false);
            //animator.SetBool("Idle", true);

            // dont change state
            return this;
        }

        agent.enabled = true;   
        aIStateManager.SwitchToCollectState();
        return aIStateManager.CurrentState;
    }
}

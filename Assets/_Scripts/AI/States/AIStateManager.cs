using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateManager : MonoBehaviour
{
    [SerializeField] private AIStates currentState;
    public AIStates CurrentState { get { return currentState; }  set { currentState = value; } }
    [SerializeField] private CollectingState collectingState;
    [SerializeField] private UnloadingState unloadingState;
    [SerializeField] private BoatState boatState;



    void Update()
    {
        RunStateMachine();
    }

    private void RunStateMachine()
    {
        AIStates nextState = currentState?.RunCurrentState();

        if (nextState != null) SwitchToNextState(nextState);
    }

    public AIStates SwitchToNextState(AIStates nextState)
    {
        currentState = nextState;
        return currentState;
    }

    public void SwitchToCollectState()
    {
        currentState = collectingState;
    } 
    
    public AIStates SwitchToUnloadState()
    {
        currentState = unloadingState;
        return currentState;
    }
    
    public AIStates SwitchToBoatState()
    {
        currentState = boatState;
        return currentState;
    }
}

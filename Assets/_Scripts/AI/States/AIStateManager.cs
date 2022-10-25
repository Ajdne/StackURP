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
    [SerializeField] private FallState fallState;

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
    
    public void SwitchToUnloadState()
    {
        currentState = unloadingState;
    }
    
    public void SwitchToBoatState()
    {
        boatState.InBoat = true;
        currentState = boatState;
    }

    public void SwitchToFallState()
    {
        currentState = fallState;
    }
}

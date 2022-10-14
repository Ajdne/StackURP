using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateManager : MonoBehaviour
{
    [SerializeField] private AIStates currentState;

    void Update()
    {
        RunStateMachine();
        print("Curreent state " + currentState);
    }

    private void RunStateMachine()
    {
        AIStates nextState = currentState?.RunCurrentState();

        if (nextState != null) SwitchToNextState(nextState);
    }

    private void SwitchToNextState(AIStates nextState)
    {
        currentState = nextState;
    }
}

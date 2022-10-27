using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AIStates : MonoBehaviour
{
    [SerializeField] protected Animator animator;
    [SerializeField] protected AIStateManager aIStateManager;
    protected NavMeshAgent agent;

    // possible states
    public abstract AIStates RunCurrentState();

 
}

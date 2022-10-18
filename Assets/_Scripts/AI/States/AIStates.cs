using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIStates : MonoBehaviour
{
    [SerializeField] protected Animator animator;
    [SerializeField] protected AIStateManager aIStateManager;

    // possible states
   



    public abstract AIStates RunCurrentState();

 
}

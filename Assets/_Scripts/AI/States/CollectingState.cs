using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class CollectingState : AIStates
{
    // list of collectables that the AI is looking for
    private List<Transform> collectList = new List<Transform>();
    public List<Transform> CollectList { get { return collectList; } set { collectList = value; } }

    private NavMeshAgent agent;

    private EnemyStacking stacking;

    // --- STATES ---
    [SerializeField] private UnloadingState unloadingState;

    [SerializeField] private int stacksToCollect; // stacks needed to change the state
    public int StacksToCollect { get { return stacksToCollect; } set { stacksToCollect = value; } }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        stacking = GetComponent<EnemyStacking>();

        stacking.collectStack += RemoveFromList;
    }

    private void RemoveFromList(GameObject stack)
    {
        collectList.Remove(stack.transform);
    }

    public override AIStates RunCurrentState()
    {
        if (collectList.Count > 0)
        {
            // always search for the 1st element in the list
            agent.SetDestination(collectList[0].position);
            animator.SetBool("Idle", false);
            animator.SetBool("Run", true);
        }
        else
        {
            // move randomly - no need for this - make it so that Agent always has something to collect

            animator.SetBool("Run", false);
            animator.SetBool("Idle", true);
        }

        //check condition for state change
        //if(BridgeComplete)
        //{

        //}
        if (stacking.GetStackCount() >= stacksToCollect)
        {
            //print("Ispunio uslov za 10 ");
            return aIStateManager.SwitchToUnloadState();    
        }
        return this;
    }
}

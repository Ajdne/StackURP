using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnloadingState : AIStates
{
    [SerializeField] private CollectingState collectingState;

    //private NavMeshAgent agent;

    private Transform waypoint;
    private GameObject crossingPoint;
    private List<Transform> potentialWaypoints;
    //public List<Transform> PotentialWaypoints { get { return potentialWaypoints; } set { potentialWaypoints = value; } }

    float waitTimer;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    public void SelectWaypoint(List<Transform> crossingsList)   // this method is called when entering a platform
    {
        // pass in the list from platform trigger
        potentialWaypoints = crossingsList;

        // get one of the crossings randomly
        int randomWaypoint = Random.Range(0, potentialWaypoints.Count);
        waypoint = potentialWaypoints[randomWaypoint].transform;
    }

    public override AIStates RunCurrentState()
    {
        // print("Current waypint " + waypoint);
        // make the AI walk to set destination


        agent.SetDestination(waypoint.position);
        if (Vector3.Distance(transform.position, waypoint.position) < 2)
        {     
            waitTimer += Time.deltaTime;

            animator.SetBool("Run", false);
            animator.SetBool("Idle", true);

            if (waitTimer > 2)
            {
                // reset the timer
                waitTimer = 0;

                // change state
                aIStateManager.SwitchToCollectState();
                return aIStateManager.CurrentState;
            }
        }
        else
        {
            //run towards unloading point
            animator.SetBool("Idle", false);
            animator.SetBool("Run", true);
        }
        return this;
    }

    public void SetWaypoint(Transform transform)
    {
        waypoint = transform;
    }
}

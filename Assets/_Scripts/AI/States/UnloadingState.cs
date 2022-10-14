using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnloadingState : AIStates
{
    [SerializeField] private CollectingState collectingState;

    private NavMeshAgent agent;
    private Animator animator;

    private Transform waypoint;
    private List<Transform> potentialWaypoints;
    //public List<Transform> PotentialWaypoints { get { return potentialWaypoints; } set { potentialWaypoints = value; } }
    
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

    private void Update()
    {
        // make the AI walk to set destination
        //agent.SetDestination(waypoint.position);
    }

    public override AIStates RunCurrentState()
    {
       // print("Current waypint " + waypoint);
        // make the AI walk to set destination
        agent.SetDestination(waypoint.position);

        if (Vector3.Distance(transform.position, waypoint.position) < 2)
            //transform.position.x == waypoint.position.x && transform.position.z == waypoint.position.z)
        {
            return collectingState;
        }
        return this;
    }
}

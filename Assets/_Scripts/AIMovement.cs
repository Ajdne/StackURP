using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    // list of collectables that the AI is looking for
    private List<Transform> collectList = new List<Transform>();
    public List<Transform> CollectList { get { return collectList; } set { collectList = value; } }
    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private Animator animator;

    private EnemyStacking stacking;

    private void Start()
    {
        stacking = GetComponent<EnemyStacking>();

        stacking.collectStack += RemoveFromList;
    }

    private void Update()
    {
        print(collectList.Count);

        if(collectList.Count > 0)
        {
            // always search for the 1st element in the list
            agent.SetDestination(collectList[0].position);
            animator.SetBool("Idle", false);
            animator.SetBool("Run", true);
        }
        else
        {
            // move randomly

            //agent.Move();
            animator.SetBool("Run", false);
            animator.SetBool("Idle", true);
        }
        
    }

    private void RemoveFromList(GameObject stack)
    {
        collectList.Remove(stack.transform);
        print("stack");
    }
}

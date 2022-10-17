using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour, IMovement
{
    [SerializeField] private int lookRadius;    // just a gizmo radius

    [Space]
    [SerializeField] private int stacksToCollect;

    [SerializeField] private NavMeshAgent agent;
    public NavMeshAgent Agent { get { return agent; } }

    [SerializeField] private Animator animator;
    public Animator Animator { get { return animator; } set { animator = value; } }

    
    private enum possibleStates
    {
        COLLECTING,
        UNLOADING,
        SHORTCUT
    }
    private possibleStates state;

    void Start()
    {
        state = possibleStates.COLLECTING;
    }

    void Update()
    {
        // ako skuplja, ide po listi redom, dok ne dodje do odredjenog broja
        if(state == possibleStates.COLLECTING)
        {
            
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    public void SetMovementSpeed(float value)
    {
        agent.speed = value;
    }

    public void ActivateMovement()
    {
        throw new System.NotImplementedException();
    }

    public void DeactivateMovement()
    {
        throw new System.NotImplementedException();
    }
}

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
        agent.enabled = true;
    }

    public void DeactivateMovement()
    {
        agent.enabled = false;
    }
}

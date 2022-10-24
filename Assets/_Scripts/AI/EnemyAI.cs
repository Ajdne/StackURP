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

    private Vector3 respawnPosition;
    //public Vector3 RespawnPosition { get { return respawnPosition; } set { respawnPosition = value; } }

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
    
    public void SetPlayerRespawnPosition(Vector3 resPos)
    {
        respawnPosition = resPos;
    }

    public IEnumerator RespawnPlayer()
    {
        // remove leftover stacks from player
        GetComponent<IStacking>().RemoveAllStacks();

        yield return new WaitForSeconds(0.4f);

        // need to disable movement script in order to move him
        DeactivateMovement();

        transform.rotation = Quaternion.Euler(0, 180, 0);
        transform.position = respawnPosition;

        //if (IsEndGame)
        //{
        //    playerAnimator.SetBool("Idle", false);
        //    playerAnimator.SetBool("Run", false);
        //    playerAnimator.SetBool("Dance", true);

        //    // dont activate player movement
        //    //Player.GetComponent<CapsuleCollider>().enabled = false;
        //    Player.GetComponent<Rigidbody>().isKinematic = true;
        //}
        
        ActivateMovement();

    }
}

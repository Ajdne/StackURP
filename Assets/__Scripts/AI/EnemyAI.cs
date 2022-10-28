using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AIStateManager))]
public class EnemyAI : MonoBehaviour, IMovement
{
    [SerializeField] private int lookRadius;    // just a gizmo radius

    [Space]
    [SerializeField] private int stacksToCollect;

    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private PlayerCollision collisionScript;
    [SerializeField] private Animator animator;

    [Header("Player Head"), Space(5f)]
    [SerializeField] private GameObject playerHead;

    private Vector3 respawnPosition;
    //public Vector3 RespawnPosition { get { return respawnPosition; } set { respawnPosition = value; } }
    private GameManager gm;

    private bool finalPlatform;

    private void Start()
    {
        gm = GameManager.Instance;
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
        agent.enabled = true;
        collisionScript.CanCollide = true;
        GetComponent<AIStateManager>().SwitchToCollectState();
    }

    public void DeactivateMovement()
    {
        agent.enabled = false;
        collisionScript.CanCollide = false;
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

        //if (gm.IsEndGame)
        //{
        //    animator.SetBool("Idle", false);
        //    animator.SetBool("Run", false);
        //    animator.SetBool("Dance", true);

        //    // dont activate player movement
        //    //Player.GetComponent<CapsuleCollider>().enabled = false;
        //    GetComponent<Rigidbody>().isKinematic = true;
        //}
        //else

        ActivateMovement();
    }

    public void SetPositionForVictoryEnd()
    {
        // remove leftover stacks from player
        GetComponent<IStacking>().RemoveAllStacks();

        // need to disable movement script in order to move him
        DeactivateMovement();

        transform.rotation = Quaternion.Euler(0, 180, 0);
        transform.position = respawnPosition;

        animator.Play("Dancing");

        //animator.SetBool("Idle", false);
        //animator.SetBool("Run", false);
        //animator.SetBool("Dance", true);
    }

    public void IncreaseMovementSpeed(float value)
    {
        throw new System.NotImplementedException();
    }

    public void ReachFinish(bool reachFinish)
    {
        finalPlatform = reachFinish;
    }

    public GameObject GetPlayerHead()
    {
        return playerHead;
    }
}

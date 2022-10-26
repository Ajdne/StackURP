using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private IStacking stackingScript;
    private IMovement movementScript;
    private Animator animator;

    private void Start()
    {
        stackingScript = GetComponent<IStacking>();
        movementScript = GetComponent<IMovement>();

        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && 
            stackingScript.GetStackCount() < other.gameObject.GetComponent<IStacking>().GetStackCount())
        {
            // lose stacks
            stackingScript.LoseStacks();
            //stackingScript.RemoveAllStacks();

            // stop the player from moving during animation
            movementScript.CollisionFall();

            // activate movement again in the animation

            // play animation
            animator.SetBool("Idle", false);
            animator.SetBool("Run", false);
            animator.SetBool("Collision", true);

            StartCoroutine(DeactivateAnimation());
        }
    }

    IEnumerator DeactivateAnimation()
    {
        yield return new WaitForSeconds(2f);

        // deactivate animation
        animator.SetBool("Collision", false);

        //GetComponent<FallState>().AnimationOver = true;
        movementScript.ActivateMovement();
    }
}

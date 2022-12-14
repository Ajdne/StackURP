using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class JumpInto : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private int speed;
    [SerializeField] private int rotateSpeed;
   

    private bool jumped;

    private Vector3 moveToVector;

    public void SetMoveToVector(Vector3 obj)
    {
        moveToVector = obj;
    }

    private void OnEnable()
    {
        GetComponent<Rigidbody>().isKinematic = true;

        animator.SetBool("Run", false);
        animator.SetBool("Idle", false);
        animator.SetBool("Jump", true);

        if (GetComponent<NavMeshAgent>() != null) GetComponent<NavMeshAgent>().enabled = false;
    }

    public void JumpStarted()   // method called in animation
    {
        jumped = true;
    }

    void Update()
    {
        //transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(Vector3.forward), rotateSpeed * Time.deltaTime);

        if (jumped)
        {
            transform.position = Vector3.MoveTowards(transform.position, moveToVector, speed * Time.deltaTime);

            if (transform.position == moveToVector)
            {
                animator.SetBool("Jump", false);
                animator.SetBool("Idle", true);

                // set bool
                jumped = false;
                //GetComponent<Rigidbody>().isKinematic = false;

                // disable this script
                this.enabled = false;
            }
        }
    }
}

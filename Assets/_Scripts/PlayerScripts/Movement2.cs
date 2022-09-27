using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2 : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    public Joystick Joystick { get { return joystick; } }

    [SerializeField] private CharacterController player;
    [SerializeField] private Animator animator;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    public float RotateSpeed { get { return rotateSpeed; } }

    private void Update()
    {
        Vector3 moveDirection = new Vector3(joystick.Horizontal, 0f, joystick.Vertical);

        if(joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);

            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed);

            animator.SetBool("Run", true);
            animator.SetBool("Idle", false);
        }
        else
        {
            animator.SetBool("Run", false);
            animator.SetBool("Idle", true);
        }

        player.SimpleMove(moveDirection.normalized * moveSpeed);
    }
}

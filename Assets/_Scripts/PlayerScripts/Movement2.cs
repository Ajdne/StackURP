using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2 : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    public Joystick Joystick { get { return joystick; } }

    public CharacterController player;
    [SerializeField] private Animator animator;

    [SerializeField] private float moveSpeed;
    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }
    [SerializeField] private float rotateSpeed;
    public float RotateSpeed { get { return rotateSpeed; } }

    private void Start()
    {
        player = GetComponent<CharacterController>();
        
    }

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
            animator.SetBool("Idle", true);
            animator.SetBool("Run", false);
        }
        
        player.SimpleMove(moveDirection.normalized * moveSpeed);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    [SerializeField] private int moveSpeed;
    [SerializeField] private CharacterController boatController;
    private GameManager gm;
    private Movement2 movementScript;

    [SerializeField] private Animator animator;
    private void Start()
    {
        gm = GameManager.Instance;
        movementScript = gm.Player.GetComponent<Movement2>();

        gm.Player.GetComponent<Movement2>().enabled = false;
        gm.Player.GetComponent<Rigidbody>().isKinematic = true;
        gm.Player.transform.position = transform.position + new Vector3(0, 0, 1);
        gm.Player.transform.parent = this.gameObject.transform;
    }

    void Update()
    {
        transform.Translate(new Vector3(0, 0, moveSpeed) * Time.deltaTime, Space.Self);

        Vector3 moveDirection = new Vector3(movementScript.Joystick.Horizontal, 0f, movementScript.Joystick.Vertical);

        if (movementScript.Joystick.Horizontal != 0 || movementScript.Joystick.Vertical != 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);

            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, movementScript.RotateSpeed);

            animator.SetBool("Row", true);
        }
        else animator.SetBool("Row", false);

        boatController.SimpleMove(moveDirection.normalized * moveSpeed);
    }


}

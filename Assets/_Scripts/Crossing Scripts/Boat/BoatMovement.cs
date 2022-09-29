using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    [SerializeField] private int moveSpeed;
    [SerializeField] private float rotateSpeed;

    private Movement2 movementScript;

    void Start()
    {
        movementScript = GameManager.Instance.Player.GetComponent<Movement2>();
    }

    void Update()
    {
        transform.Translate(new Vector3(0, 0, moveSpeed) * Time.deltaTime, Space.Self);

        if (movementScript.Joystick.Horizontal != 0) // || movementScript.Joystick.Vertical != 0)
        {
            transform.Rotate(new Vector3(0, rotateSpeed * movementScript.Joystick.Horizontal * Time.deltaTime, 0), Space.Self);
        }
    }
}

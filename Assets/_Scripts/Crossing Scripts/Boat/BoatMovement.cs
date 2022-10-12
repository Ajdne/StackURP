using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    [SerializeField] private int moveSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private CharacterController boatController;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject destroyParticle;
    private Movement2 movementScript;
    private GameManager gm;

    void Start()
    {
        movementScript = GameManager.Instance.Player.GetComponent<Movement2>();
        gm = GameManager.Instance;

        //animator = GetComponent<Animator>();
    }

    void Update()
    {
        transform.Translate(new Vector3(0, 0, moveSpeed) * Time.deltaTime, Space.Self);

        if (movementScript.Joystick.Horizontal != 0) // || movementScript.Joystick.Vertical != 0)
        {
            transform.Rotate(new Vector3(0, rotateSpeed * movementScript.Joystick.Horizontal * Time.deltaTime, 0), Space.World);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7) // BOAT layer
        {
            // smoke
            destroyParticle.SetActive(true);


            // player jump animation and move to the platform
            //gm.Player.GetComponent<JumpInto>().SetMoveToVector(other.transform.position);
            //gm.Player.GetComponent<JumpInto>().enabled = true;

            // enable player movement
            movementScript.enabled = true;
            gm.Player.GetComponent<Rigidbody>().isKinematic = false;

            // remove the player parent
            gm.Player.transform.parent = null;

            // move the player to the platform
            
            
            // PutPlayerOutOfBoat();


            //gm.SetPlayerPosition(transform.position + new Vector3(0, 0, 5));

            // deactivate the ship
            Invoke("DestroyBoat", 0.5f);
        }
        
    }

    private void DestroyBoat()
    {
        Destroy(this.gameObject);
    }
}

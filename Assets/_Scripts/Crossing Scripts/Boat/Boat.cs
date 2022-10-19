using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boat : MonoBehaviour
{
    [SerializeField] private GameObject rowerMan;

    [SerializeField] private BoatMovement boatMovement;
    [SerializeField] private Animator rowAnimator;
    [SerializeField] private GameObject destroyParticle;

   // private AIStateManager aiStateManager;

    private Movement2 movementScript;
    private GameManager gm;

    private GameObject playerToTransport;

    private void Start()
    {
        gm = GameManager.Instance;
        //movementScript = gm.Player.GetComponent<Movement2>();

        // start the player onboarding
        StartCoroutine(SpawnRowerMan());
    }

    IEnumerator SpawnRowerMan()
    {
        yield return new WaitForSeconds(0.5f);
        // deactivate player movement, collider etc
        gm.RevertPlayerControls(playerToTransport); // kinematic n stuff



     
        //if ((aiStateManager=playerToTransport.GetComponent<AIStateManager>()) != null)
        //    aiStateManager.SwitchToBoatState();

        // activate player JumpInto script
        playerToTransport.GetComponent<JumpInto>().SetMoveToVector(transform.position + new Vector3(0, 0, 1));
        
        // position the player
        playerToTransport.GetComponent<JumpInto>().enabled = true;

        yield return new WaitForSeconds(1);

        playerToTransport.transform.parent = this.gameObject.transform;

        yield return new WaitForSeconds(1);
        // activate rowing stickman
        rowerMan.SetActive(true);

        yield return new WaitForSeconds(1);
        // start rowing animation
        rowAnimator.enabled = true;

        yield return new WaitForSeconds(0.5f);
        // activate movement script
        boatMovement.enabled = true;
    }

    public void SetPlayerToTransport(GameObject player)
    {
        playerToTransport = player;
    }

    //------- OVDE CE BITI PROMENA ------------------------
    public void PutPlayerOutOfBoat()
    {
        //playerToTransport.GetComponent<Movement2>().player.enabled = false;

        // remove the player parent
        playerToTransport.transform.parent = null;

        playerToTransport.transform.position = transform.position + new Vector3(0, 0, 5);

        // activate collider again
        playerToTransport.GetComponent<CapsuleCollider>().enabled = true;

        // enable player movement
        playerToTransport.GetComponent<Rigidbody>().isKinematic = false;
        playerToTransport.GetComponent<IMovement>().ActivateMovement();

        //playerToTransport.GetComponent<Movement2>().player.enabled = true;
        //  playerToTransport.GetComponent<Movement2>().enabled = true;

        //Deactivate BoatState on Enemy
        BoatState boatState;
        if ((boatState = playerToTransport.GetComponent<BoatState>()) != null)
            boatState.InBoat = false;

    }
    // --------------------------------------------------------



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7) // BOAT collision layer
        {
            // smoke
            destroyParticle.SetActive(true);

            // player jump animation and move to the platform
            //gm.Player.GetComponent<JumpInto>().SetMoveToVector(other.transform.position);
            //gm.Player.GetComponent<JumpInto>().enabled = true;

            

            // move the player to the platform
            PutPlayerOutOfBoat();

            // deactivate the ship
            Invoke("DestroyBoat", 0.5f);
        }

    }

    private void DestroyBoat()
    {
        Destroy(this.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boat : MonoBehaviour
{
    [SerializeField] private GameObject rowerMan;

    [SerializeField] private BoatMovement boatMovement;
    [SerializeField] private Animator rowAnimator;

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
        gm.RevertPlayerControls();

        // activate player JumpInto script
        gm.Player.GetComponent<JumpInto>().SetMoveToVector(transform.position + new Vector3(0, 0, 1));
        gm.Player.GetComponent<JumpInto>().enabled = true;

        yield return new WaitForSeconds(1);
        // position the player
        

        //gm.Player.transform.position = transform.position + new Vector3(0, 0, 1);
        gm.Player.transform.parent = this.gameObject.transform;

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

    public void PutPlayerOutOfBoat()
    {
        playerToTransport.GetComponent<Movement2>().enabled = false;
        playerToTransport.transform.position = transform.position + new Vector3(0, 0, 5);
        playerToTransport.GetComponent<Movement2>().enabled = true;
    }

    public void SetPlayerToTransport(GameObject player)
    {
        playerToTransport = player;

        // OOOOOOOOOOOOVVOOOOOOOOOOOOOOOOOOOO
    }
}

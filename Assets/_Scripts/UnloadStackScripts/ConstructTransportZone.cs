using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructTransportZone : MonoBehaviour
{
    [SerializeField] private List<GameObject> elements = new List<GameObject>();
    [SerializeField] private GameObject fragmentsParent;
    [SerializeField] private GameObject completeTransport;
    [SerializeField] private GameObject fragmentParticle;

    [SerializeField] private GameObject spawnLocation;

    private float stayTimer;

    private GameObject playerOnTrigger = null;
    public GameObject PlayerOnTrigger { get { return playerOnTrigger; } }

    // use this variable to count active elements
    private int elementCounter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && playerOnTrigger == null)
        {
            // save the player object
            playerOnTrigger = other.gameObject;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player") && playerOnTrigger == other.gameObject)
        {
            stayTimer += Time.deltaTime;

            if (stayTimer > 0.1f && elementCounter != elements.Count && other.GetComponent<IStacking>().GetStackCount() > 0)
            {
                // remove money from the stack
                other.gameObject.GetComponent<IStacking>().RemoveMoneyToProperty(elements[elementCounter].transform.position, true);

                ActivateBoatFragments(other.gameObject);
            }
        }
    }

    private void ActivateBoatFragments(GameObject player)
    {
        // activate the element
        elements[elementCounter].SetActive(true);

        // spawn fragment particles
        Instantiate(fragmentParticle, elements[elementCounter].transform.position, Quaternion.identity);

        elementCounter++;

        // reset the timer
        stayTimer = 0;

        if (elementCounter == elements.Count)
        {
            // deactivate box collider of this zone
            //this.gameObject.GetComponent<BoxCollider>().enabled = false;

            // deactivate fragments
            fragmentsParent.SetActive(false);

            // spawn complete transport model, play animation
            GameObject boat = Instantiate(completeTransport, spawnLocation.transform.position, Quaternion.identity);
            boat.GetComponent<Boat>().SetPlayerToTransport(player);
            //completeTransport.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // reset the timer
            stayTimer = 0;

            StartCoroutine(RespawnFragments());
        }
    }

    private IEnumerator RespawnFragments()
    {
        yield return new WaitForSeconds(0.5f);

        // when the player leaves and the boat is already constructed
        if (elementCounter == elements.Count)
        {
            // reset the counter
            elementCounter = 0;

            // re-enable spawning of fragments again
            RespawnFragments();


            // activaye parent
            fragmentsParent.SetActive(true);

            // deactivate every fragment
            foreach (GameObject fragment in elements)
            {
                fragment.SetActive(false);
            }
        }

        // reset the last player on trigger
        playerOnTrigger = null;
    }
}

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

    // use this variable to count active elements
    private int elementCounter;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) // player layer
        {
            stayTimer += Time.deltaTime;

            if (stayTimer > 0.1f && other.GetComponent<Stacking>().GetStackCount() > 0 && elementCounter != elements.Count)
            {
                // remove money from the stack
                other.GetComponent<Stacking>().RemoveMoneyToProperty(elements[elementCounter].transform.position, true);

                // activate the element
                elements[elementCounter].SetActive(true);

                // spawn fragment particles
                Instantiate(fragmentParticle, elements[elementCounter].transform.position, Quaternion.identity);

                elementCounter++;

                // reset the timer
                stayTimer = 0;

                if(elementCounter == elements.Count)
                {
                    // deactivate box collider of this zone
                    //this.gameObject.GetComponent<BoxCollider>().enabled = false;

                    // deactivate fragments
                    fragmentsParent.SetActive(false);

                    // spawn complete transport model, play animation
                    Instantiate(completeTransport, spawnLocation.transform.position, Quaternion.identity);
                    //completeTransport.SetActive(true);
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // player layer
        {
            // reset the timer
            stayTimer = 0;

            // when the player leaves and the boat is already constructed
            if (elementCounter == elements.Count)
            {
                // reset the counter
                elementCounter = 0;

                // re-enable spawning of fragments again
                RespawnFragments();
            }
        }
    }

    private void RespawnFragments()
    {
        // activaye parent
        fragmentsParent.SetActive(true);

        // deactivate every fragment
        foreach(GameObject fragment in elements)
        {
            fragment.SetActive(false);
        }
    }

}

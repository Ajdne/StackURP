using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructTransportZone : MonoBehaviour
{
    [SerializeField] private List<GameObject> elements = new List<GameObject>();
    [SerializeField] private GameObject fragmentsParent;
    [SerializeField] private GameObject completeTransport;

    private float stayTimer;

    // use this variable to count active elements
    private int elementCounter;

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 3) // player layer
        {
            stayTimer += Time.deltaTime;

            if (stayTimer > 0.1f && other.GetComponent<Stacking>().GetStackCount() > 0)
            {
                // remove money from the stack
                other.GetComponent<Stacking>().RemoveMoneyToProperty(elements[elementCounter].transform.position, true);

                // activate the element
                elements[elementCounter].SetActive(true);

                elementCounter++;

                // reset the timer
                stayTimer = 0;

                if(elementCounter == elements.Count)
                {
                    // deactivate box collider of this zone
                    this.gameObject.GetComponent<BoxCollider>().enabled = false;

                    // activate complete transport model, play animation
                    completeTransport.SetActive(true);

                    // deactivate elements
                    fragmentsParent.SetActive(false);
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 3) // player layer
        {
            // reset the timer
            stayTimer = 0;
        }
    }

}

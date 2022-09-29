using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PropertyZone : MonoBehaviour
{
    [Header("Connected Prefabs")]
    [SerializeField] private GameObject propertyObj;

    [SerializeField] private int stacksNeeded;
    [SerializeField] private Animator gateAnimator;

    private float stayTimer;

    //[SerializeField] private GameObject grayModel;
    //[SerializeField] private GameObject coloredModel;

    private List<GameObject> buildElements = new List<GameObject>();
    public List<GameObject> BuildElements { get { return buildElements; } set { buildElements = value; } }

    private int buildCount = 0;

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 3) // player layer
        {
            stayTimer += Time.deltaTime;

            if (stayTimer > 0.05f && other.GetComponent<Stacking>().GetStackCount() > 0 && buildCount < stacksNeeded)
            {
                buildCount++;
                // remove money from the stack
                other.GetComponent<Stacking>().RemoveMoneyToProperty(propertyObj.transform.position + new Vector3(0, 0 , -5 + buildCount * 0.75f), false);

                stayTimer = 0;

                if(buildCount == stacksNeeded)
                {
                    gateAnimator.enabled = true;
                }
            }
        }
    }


}

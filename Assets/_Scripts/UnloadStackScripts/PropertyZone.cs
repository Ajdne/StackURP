using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PropertyZone : MonoBehaviour
{
    [Header("Connected Prefabs")]
    [SerializeField] private GameObject propertyObj;
    [SerializeField] private GameObject endGate;
    
    private int stacksToUnlockGate;
    private float stayTimer;

    private List<GameObject> buildElements = new List<GameObject>();
    public List<GameObject> BuildElements { get { return buildElements; } set { buildElements = value; } }

    private int buildCount = 0;

    private void Start()
    {
        stacksToUnlockGate = endGate.GetComponent<BridgeGate>().StacksNeeded;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) // player layer
        {
            stayTimer += Time.deltaTime;

            if (stayTimer > 0.05f && other.GetComponent<Stacking>().GetStackCount() > 0 && buildCount < stacksToUnlockGate)
            {
                buildCount++;
                // remove money from the stack
                other.GetComponent<Stacking>().RemoveMoneyToProperty(propertyObj.transform.position + new Vector3(0, 0 , -8 + buildCount * 0.75f), false);

                stayTimer = 0;
            }
        }
    }

    public bool IsGateUnlocked(int stacks)
    {
        if (buildCount == stacks) return true;
        return false;
    }

}

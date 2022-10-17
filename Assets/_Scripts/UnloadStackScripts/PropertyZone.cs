using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PropertyZone : MonoBehaviour
{
    [Header("Connected Prefabs")]
    [SerializeField] private GameObject propertyObj;
    [SerializeField] private GameObject endGate;

    // waypoint for AI
    [SerializeField] private Transform endPoint;

    private int stacksToUnlockGate;
    private float stayTimer;

    //private List<GameObject> buildElements = new List<GameObject>();
    //public List<GameObject> BuildElements { get { return buildElements; } set { buildElements = value; } }

    private int buildCount = 0;

    public Action<Transform> BridgeComplete;

// - - - -- - -  THIS SCRIPT IS PLACED ON A BRIDGE PREFAB - - - -- - - - -

    private void Start()
    {
        stacksToUnlockGate = endGate.GetComponent<BridgeGate>().StacksNeeded;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) // player layer
        {
            if (other.gameObject.layer != 10)
                other.gameObject.GetComponent<UnloadingState>().SetCrossingPoint(this.gameObject);
               
            
            stayTimer += Time.deltaTime;

            if (stayTimer > 0.05f)
            {
                if(other.GetComponent<IStacking>().GetStackCount() > 0 && buildCount < stacksToUnlockGate)
                {
                    buildCount++;
                    // remove money from the stack
                    other.GetComponent<IStacking>().RemoveMoneyToProperty(propertyObj.transform.position + new Vector3(0, 0, -8 + buildCount * 0.75f), false);

                    stayTimer = 0;
                }
            }
         
            if(buildCount == stacksToUnlockGate)
            {
                BridgeComplete?.Invoke(endPoint);
            }
        }
    }

    public bool IsGateUnlocked(int stacks)
    {
        if (buildCount == stacks) return true;
        return false;
    }

}

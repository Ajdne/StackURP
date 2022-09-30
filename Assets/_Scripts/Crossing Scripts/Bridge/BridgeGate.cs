using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeGate : MonoBehaviour
{
    [SerializeField] private Animator gateAnimator;
    [SerializeField] private PropertyZone bridgeScript;

    [SerializeField] private GameObject particles;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3) // player layer
        {
            if (bridgeScript.IsGateUnlocked())
            {
                // activate gate open animation;
                gateAnimator.enabled = true;
            }
        }
    }
}

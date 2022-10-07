using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeGate : MonoBehaviour
{
    [SerializeField] private int stacksNeeded;
    public int StacksNeeded { get { return stacksNeeded; } }
    [SerializeField] private Animator gateAnimator;
    [SerializeField] private PropertyZone bridgeScript;

    [SerializeField] private GameObject particles;

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer == 3) // player layer
        {
            if (bridgeScript.IsGateUnlocked(stacksNeeded))
            {
                StartCoroutine(OpenGate());
            }
        }
    }

    IEnumerator OpenGate()
    {
        yield return new WaitForSeconds(0.5f);
        // activate gate open animation;
        gateAnimator.enabled = true;
    }
}

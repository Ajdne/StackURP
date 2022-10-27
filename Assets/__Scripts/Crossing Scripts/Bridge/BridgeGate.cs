using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeGate : MonoBehaviour
{
    [SerializeField] private int stacksNeeded;
    public int StacksNeeded { get { return stacksNeeded; } }

    [SerializeField] private Animator gateAnimator;
    [SerializeField] private PropertyZone bridgeScript;

    //[SerializeField] private GameObject particles;

    [SerializeField] private ParticleSystem particleSys1;
    [SerializeField] private ParticleSystem particleSys2;

    [Header("Material Settings"), Space(2f)]
    [SerializeField] private MaterialHolderSO matHolder;
    [SerializeField] private List<MeshRenderer> meshRenderers;
    private Material playerMat;

    private bool isTriggered;

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player")) // player layer
        {
            if (bridgeScript.IsGateUnlocked(stacksNeeded) && !isTriggered)
            {
                // take the player material from the mat holder 
                playerMat = matHolder.PlayerMaterials[other.gameObject.layer - 10];

                StartCoroutine(OpenGate());

                // reset the trigger - need it to prevent color change on every player enter
                isTriggered = true;
            }
        }
    }

    private IEnumerator OpenGate()
    {
        for (int i = 0; i < meshRenderers.Count; i++)
        {
            meshRenderers[i].material = playerMat;
        }
        particleSys1.startColor = playerMat.color;
        particleSys2.startColor = playerMat.color;

        yield return new WaitForSeconds(0.5f);

        // activate gate open animation;
        gateAnimator.enabled = true;
    }
}

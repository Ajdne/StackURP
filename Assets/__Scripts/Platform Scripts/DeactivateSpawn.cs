using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateSpawn : MonoBehaviour
{
    [SerializeField] private StackSpawn stackSpawn;
    
    private GameManager gm;
    
    // --- This is called when the player leaves this platform in order to optimise performance ---
    private void Start()
    {
        gm = GameManager.Instance;

        // for safety
        if(stackSpawn == null) GetComponentInParent<StackSpawn>();
    }

    private void OnTriggerEnter(Collider other)
    {
        int playerIndex = other.gameObject.layer - 10;

        if(other.CompareTag("Player") && stackSpawn.UnlockedStacksToSpawn.Contains(gm.StackPrefs[playerIndex]))
        {
            // remove those stacks from the stack spawn list
            stackSpawn.UnlockedStacksToSpawn.Remove(gm.StackPrefs[playerIndex]);

            // disable the script if the list is empty
            if (stackSpawn.UnlockedStacksToSpawn.Count == 0) stackSpawn.enabled = false;

            if(playerIndex == 0) // blue player
            {
                // disable spawning of additional stacks for blue player
                GetComponentInParent<AdditionalStackSpawn>().enabled = false;
            }
        }
    }
}

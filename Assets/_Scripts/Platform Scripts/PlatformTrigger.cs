using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{
    private Transform respawnLocation;

    private StackSpawn stackSpawnScript;
    private bool canShortcut;

    private bool isTriggered;
    private bool isOnPlatform;

    private int layerValue;

    private GameManager gm;
    private void Start()
    {
        gm = GameManager.Instance;

        respawnLocation = GetComponentInParent<Platforms>().GetRespawnLocation();
        canShortcut = GetComponentInParent<Platforms>().CanUseShortcut;
        stackSpawnScript = GetComponentInParent<StackSpawn>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // player layer
        {
            // pass the layer of the player that reached the trigger
            // player stack prefabs are in a list in Game manager, and they are arranged as the layer number of players - 10
            if (!stackSpawnScript.UnlockedStacksToSpawn.Contains(gm.StackPrefs[other.gameObject.layer - 10]))
            {
                // pass in the prefab that we want to spawn
                stackSpawnScript.UnlockedStacksToSpawn.Add(gm.StackPrefs[other.gameObject.layer - 10]);

                // activate stack spawning - the initial stacks are spawned ONLY ONCE
                stackSpawnScript.enabled = true;

                // spawn several stacks when the platform is entered
                stackSpawnScript.SpawnInitialStacks(other.gameObject);
                print(stackSpawnScript);
            }

            // activate stack spawning - the initial stacks are spawned ONLY ONCE
            stackSpawnScript.enabled = true;

            // set respawn position
            GameManager.Instance.PlayerRespawnPos = respawnLocation.position;

            //GameManager.Instance.PlayerRespawnPos = transform.position + new Vector3(0, 0, 3);

            if (!canShortcut && !isTriggered)
            {
                StartCoroutine(DisableShortCutRun(other));

                isTriggered = true;
            }

            // enable/disable shortcut run script
            other.gameObject.GetComponent<ShortCutRun>().enabled = canShortcut;
        }
    }

    IEnumerator DisableShortCutRun(Collider other)
    {
        yield return new WaitForSeconds(0.5f);

        // if boost is already disabled
        if (!other.gameObject.GetComponent<ShortCutRun>().GotBoost) yield break;

        //first disable boost
        other.gameObject.GetComponent<ShortCutRun>().DisableBoost();

        // then disable the script
        other.gameObject.GetComponent<ShortCutRun>().enabled = canShortcut;
    }
}

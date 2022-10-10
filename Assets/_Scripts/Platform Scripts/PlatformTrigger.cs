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
            // pass the tag of the player that reached the trigger
            if(!stackSpawnScript.UnlockedStacksToSpawn.Contains(gm.StackPrefs[other.gameObject.layer - 10]))
            {
                // pass in the prefab that we want to spawn
                stackSpawnScript.UnlockedStacksToSpawn.Add(gm.StackPrefs[other.gameObject.layer - 10]);


                // PROBLEM JE REDOSLED POVECANJA LISTE


                // spawn several stacks when the platform is entered
                //stackSpawnScript.SpawnInitialStacks(gm.StackPrefs[other.gameObject.layer - 10]);
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

        //first disable boost
        other.gameObject.GetComponent<ShortCutRun>().DisableBoost();

        // then disable the script
        other.gameObject.GetComponent<ShortCutRun>().enabled = canShortcut;
    }
}

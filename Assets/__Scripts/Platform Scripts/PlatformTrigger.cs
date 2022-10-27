using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{
    private Vector3 respawnLocation;

    private StackSpawn stackSpawnScript;
    private bool canShortcut;

    private bool isTriggered;

    private List<Transform> crossings;

    private int stacksToCollect;
    private AdditionalStackSpawn additionalSpawn;
    public AdditionalStackSpawn AdditionalSpawn { set { additionalSpawn = value; } }

    // a list to save players who triggered it
    private List<GameObject> playersTriggered = new List<GameObject>();

    private GameManager gm;

    private void Start()
    {
        gm = GameManager.Instance;

        respawnLocation = GetComponentInParent<Platforms>().RespawnPosObj.transform.position;
        canShortcut = GetComponentInParent<Platforms>().CanUseShortcut;
        stackSpawnScript = GetComponentInParent<StackSpawn>();

        crossings = GetComponentInParent<Platforms>().WaypointLocations;

        // pass the value of stacks needed on every platform
        stacksToCollect = GetComponentInParent<Platforms>().StacksToCollect;

        // get additional stack spawn script for blue player
        additionalSpawn = GetComponentInParent<AdditionalStackSpawn>();
    }

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log(other.gameObject);
        if (other.CompareTag("Player")) // player layer
        {
            // if the player passes the trigger for the first time
            if(playersTriggered.Contains(other.gameObject))
            {
                return;
            }
            else
            {
                #region Updating the list of stacks to spawn
                // ---------------- PART FOR SPAWNING STACKS ----------------
                // add this player to the list
                playersTriggered.Add(other.gameObject);

                // pass the layer of the player that reached the trigger
                // player stack prefabs are in a list in Game manager, and they are arranged as the layer number of players - 10

                // pass in the prefab that we want to spawn
                stackSpawnScript.UnlockedStacksToSpawn.Add(gm.StackPrefs[other.gameObject.layer - 10]);

                // activate stack spawning - the initial stacks are spawned ONLY ONCE
                stackSpawnScript.enabled = true;

                // spawn several stacks when the platform is entered
                stackSpawnScript.SpawnInitialStacks(other.gameObject);
                // ---------------- -------------------------- ----------------
                #endregion

                // save respawn position
                other.gameObject.GetComponent<IMovement>().SetPlayerRespawnPosition(respawnLocation);

                if (other.gameObject.layer == 10)  // blue player layer
                {
                    // activate aditional stack spawn script for blue player
                    additionalSpawn.enabled = true;
                }
                // if its an agent
                else
                {
                    // clear the list of stacks that the agent wants to collect
                    other.GetComponent<CollectingState>().CollectList.Clear();

                    // pass the list of crossings to the AI 
                    other.GetComponent<UnloadingState>().SelectWaypoint(crossings);

                    // pass the value of stacks needed for Ai to collect
                    other.GetComponent<CollectingState>().StacksToCollect = stacksToCollect;
                }

                if (!canShortcut) // && !isTriggered)
                {
                    StartCoroutine(DisableShortCutRun(other));

                    isTriggered = true;
                }

                // enable/disable shortcut run script
                other.gameObject.GetComponent<ShortCutRun>().enabled = canShortcut;

                // if all players triggered this object
                if(playersTriggered.Count == 4)
                {
                    // disable this script
                    this.enabled = false;
                }
            }
        }
    }

    IEnumerator DisableShortCutRun(Collider other)
    {
        yield return new WaitForSeconds(0.5f);

        // if boost is already disabled
        if (other.gameObject.GetComponent<ShortCutRun>().GotBoost)
        {
            //first disable boost
            other.gameObject.GetComponent<ShortCutRun>().DisableBoost();
        }

        // then disable the script
        //other.gameObject.GetComponent<ShortCutRun>().enabled = canShortcut;
    }
}

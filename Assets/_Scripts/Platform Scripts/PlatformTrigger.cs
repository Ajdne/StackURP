using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{
    [SerializeField] private Transform respawnLocation;

    private StackSpawn stackSpawnScript;
    private bool canShortcut;

    private bool isTriggered;

    private void Start()
    {
        canShortcut = GetComponentInParent<Platforms>().CanUseShortcut;
        //stackSpawnScript = GetComponentInParent<StackSpawn>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3) // player layer
        {
            // activate stack spawning
            GetComponentInParent<StackSpawn>().enabled = true;
            //stackSpawnScript.enabled = true;

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{
    //[SerializeField] 
    private StackSpawn stackSpawnScript;
    private bool canShortcut;

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
            GameManager.Instance.PlayerRespawnPos = transform.position + new Vector3(0, 1, 4);

            // enable/disable shortcut run script
            other.gameObject.GetComponent<ShortCutRun>().enabled = canShortcut;
        }
    }
}

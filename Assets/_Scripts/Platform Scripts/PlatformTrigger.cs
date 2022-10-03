using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{
    private StackSpawn stackSpawnScript;

    private void Start()
    {
        stackSpawnScript = GetComponentInParent<StackSpawn>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3) // player layer
        {
            // activate stack spawning
            stackSpawnScript.enabled = true;

            // set respawn position
            GameManager.Instance.PlayerRespawnPos = transform.position + new Vector3(0, 2, 0);
        }
    }
}

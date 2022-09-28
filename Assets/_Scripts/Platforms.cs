using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour
{
    [SerializeField] private List<GameObject> crossingLocations;
    [SerializeField] private StackSpawn stackSpawnScript;
    [SerializeField] private GameObject respawnPosObj;

    private void Start()
    {
        if (stackSpawnScript == null)
        {
            stackSpawnScript = GetComponent<StackSpawn>();
            stackSpawnScript.enabled = false;
        }

        int randomCrossing = Random.Range(0, GameManager.Instance.Crossings.Count);

        for (int i = 0; i < crossingLocations.Count; i++)
        {
            Instantiate(GameManager.Instance.Crossings[randomCrossing], crossingLocations[i].transform.position + new Vector3(0, -0.5f, 5f), Quaternion.identity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3) // player layer
        {
            // activate stack spawning
            stackSpawnScript.enabled = true;

            // set respawn position
            GameManager.Instance.PlayerRespawnPos = respawnPosObj.transform.position;
            //print(GameManager.Instance.PlayerRespawnPos);
        }
    }
}

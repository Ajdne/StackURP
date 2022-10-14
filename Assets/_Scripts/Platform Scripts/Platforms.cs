using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour
{
    [SerializeField] private List<Transform> waypointLocations;
    public List<Transform> WaypointLocations { get { return waypointLocations; } }

    [SerializeField] private List<Transform> spawnCrossingLocations;

    [SerializeField] private StackSpawn stackSpawnScript;
    [SerializeField] private GameObject respawnPosObj;
    [SerializeField] private GameObject triggerCollider;

    [SerializeField] private bool canUseShortcut;
    public bool CanUseShortcut { get { return canUseShortcut; } }
   

    private void Start()
    {
        //if (stackSpawnScript == null)
        //{
        //    stackSpawnScript = GetComponent<StackSpawn>();
        //    stackSpawnScript.enabled = false;
        //}

        int randomCrossing = Random.Range(0, GameManager.Instance.Crossings.Count);

        for (int i = 0; i < spawnCrossingLocations.Count; i++)
        {
            Instantiate(GameManager.Instance.Crossings[randomCrossing], spawnCrossingLocations[i].position + new Vector3(0, 0, 0), Quaternion.identity);
        }
    }

    public Transform GetRespawnLocation()
    {
        return respawnPosObj.transform;
    }
}

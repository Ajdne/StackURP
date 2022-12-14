using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour
{
    [SerializeField] private List<Transform> waypointLocations;
    public List<Transform> WaypointLocations { get { return waypointLocations; } }

    // [SerializeField] private List<Transform> spawnCrossingLocations;

    [SerializeField] private StackSpawn stackSpawnScript;

    [SerializeField] private GameObject respawnPosObj;
    public GameObject RespawnPosObj { get { return respawnPosObj; } }

    [SerializeField] private GameObject platformActivationTrigger;

    [SerializeField] private bool canUseShortcut;
    public bool CanUseShortcut { get { return canUseShortcut; } }

    [SerializeField] private int stacksToCollect;
    public int StacksToCollect { get { return stacksToCollect; } }

    private void Start()
    {
        //int randomCrossing = Random.Range(0, GameManager.Instance.Crossings.Count);

        //for (int i = 0; i < spawnCrossingLocations.Count; i++)
        //{
        //    Instantiate(GameManager.Instance.Crossings[randomCrossing], spawnCrossingLocations[i].position + new Vector3(0, 0, 0), Quaternion.identity);
        //}
    }
}

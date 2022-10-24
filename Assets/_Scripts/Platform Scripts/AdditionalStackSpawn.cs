using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalStackSpawn : MonoBehaviour
{
    private GameObject stackPrefab;
    private StackSpawn spawnScript;

    float timePassed;
    [SerializeField] private float spawnTime;

    void Start()
    {
        stackPrefab = GameManager.Instance.StackPrefs[0]; // the first stack prefab - blue
        spawnScript = GetComponent<StackSpawn>();
    }

    void Update()
    {
        timePassed += Time.deltaTime;

        if (spawnTime < timePassed)
        {
            spawnScript.SpawnWithRayCast(stackPrefab);

            timePassed = 0;
        }
    }
}

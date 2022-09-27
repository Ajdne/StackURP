using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackSpawn : MonoBehaviour
{
    [SerializeField] private int initialNumberOfStacks;
    [SerializeField] private int maxStacks;

    [SerializeField] private float stackSpawnTime;
    private float spawnTime;

    //public List<GameObject> stacksOnPlat = new List<GameObject>();

    private GameManager gm;

    void Start()
    {
        gm = GameManager.Instance;

        for (int i = 0; i < initialNumberOfStacks; i++)
        {
            // add it to the list
            //stacksOnPlat.Add(gm.SpawnOnRandomPosition(gm.StackPref, this.gameObject));
            gm.SpawnOnRandomPosition(gm.StackPref, this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        spawnTime += Time.deltaTime;

        if(spawnTime > stackSpawnTime) // && stacksOnPlat.Count < maxStacks)
        {
            // add it to the list
            //stacksOnPlat.Add(gm.SpawnOnRandomPosition(gm.StackPref, this.gameObject));

            gm.SpawnOnRandomPosition(gm.StackPref, this.gameObject);

            // reset the timer
            spawnTime = 0;
        }
    }
}

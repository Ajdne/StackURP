using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackSpawn : MonoBehaviour
{
    [SerializeField] private int initialNumberOfStacks;
    //[SerializeField] private int maxStacks;

    [SerializeField] private float stackSpawnTime;
    private float spawnTime;

    private MeshCollider collider;

    // stacks to spawn over time with raycast - unlocked when a player reaches the platform
    private List<GameObject> unlockedStacksToSpawn = new List<GameObject>();
    public List<GameObject> UnlockedStacksToSpawn { get { return unlockedStacksToSpawn; } set { unlockedStacksToSpawn = value; } }

    private RaycastHit hit;
    private Ray ray;
    private GameManager gm;

    void Start()
    {
        gm = GameManager.Instance;

        collider = GetComponent<MeshCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < unlockedStacksToSpawn.Count; i++)
        {
            spawnTime += Time.deltaTime;

            if (spawnTime > stackSpawnTime) // && stacksOnPlat.Count < maxStacks)
            {
                SpawnWithRayCast(unlockedStacksToSpawn[i]);
             
                // add it to the list
                //stacksOnPlat.Add(gm.SpawnOnRandomPosition(gm.StackPref, this.gameObject));

                //gm.SpawnOnRandomPosition(gm.StackPref, this.gameObject);
                //RandomPosition(this.gameObject.GetComponent<Collider>());
                
                // reset the timer
                spawnTime = 0;
            }
        }    
    }

    public void SpawnInitialStacks(GameObject player)
    {
        print(gm.GetObjectFormList(0));
        print(gm.GetObjectFormList(1));
        // spawn a number of initial stacks when the player reaches the platform
        for (int i = 0; i < initialNumberOfStacks; i++)
        {
            SpawnWithRayCast(gm.GetObjectFormList(player.gameObject.layer - 10));
            print("2");
            //print(gm.StackPrefs[player.gameObject.layer - 10].ToString());
            //SpawnWithRayCast(gm.StackPrefs[player.gameObject.layer - 10]);
        }
    }

    private void SpawnWithRayCast(GameObject stackPref)
    {
        print("3");
        int randomX = Random.Range((int)collider.bounds.min.x + 2, (int)collider.bounds.max.x - 1);
        int randomZ = Random.Range((int)collider.bounds.min.z + 2, (int)collider.bounds.max.z - 1);

        ray = new Ray(new Vector3(randomX, 10, randomZ), Vector3.down);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.layer == 6) // platform layer
            {
                Instantiate(stackPref, hit.point + new Vector3(0, stackPref.transform.localScale.y /2, 0), Quaternion.identity);
            }
        }
    }

    // ovo moze sa listom kolajdera
    //public Vector3 RandomPosition(Collider region)
    //{
    //    //get random Z and X axis values for the random position within the chosen area
    //    float xRandom = Random.Range(region.bounds.min.x, region.bounds.max.x);
    //    float zRandom = Random.Range(region.bounds.min.z, region.bounds.max.z);

    //    Vector3 randomSpawnPosition = new Vector3(xRandom, 0.5f, zRandom); //combine the 2 gotten coordinates to get the final position Vector
    //    Instantiate(gm.StackPref, randomSpawnPosition, Quaternion.identity);
    //    return randomSpawnPosition;
    //}

}

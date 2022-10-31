using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AdditionalStackSpawn))]
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

    private void Awake()
    {
        // for some reason, this wont work in start (it works but we need it sooner)
        collider = GetComponent<MeshCollider>();

        gm = GameManager.Instance;
    }

    void Update()
    {
        spawnTime += Time.deltaTime;

        if (spawnTime > stackSpawnTime) // && stacksOnPlat.Count < maxStacks)
        {
            for (int i = 0; i < unlockedStacksToSpawn.Count; i++)
            {
                SpawnWithRayCast(unlockedStacksToSpawn[i]);                
            }

            // reset the timer
            spawnTime = 0;
        }    
    }

    public void SpawnInitialStacks(GameObject player)
    {
        // spawn a number of initial stacks when the player reaches the platform
        for (int i = 0; i < initialNumberOfStacks; i++)
        {
            SpawnWithRayCast(GameManager.Instance.GetObjectFromList(player.gameObject.layer - 10));

            //print(gm.StackPrefs[player.gameObject.layer - 10].ToString());
            //SpawnWithRayCast(gm.StackPrefs[player.gameObject.layer - 10]);
        }
    }

    public void SpawnWithRayCast(GameObject stackPref)
    {
        int randomX = Random.Range((int)collider.bounds.min.x + 2, (int)collider.bounds.max.x - 1);
        int randomZ = Random.Range((int)collider.bounds.min.z + 2, (int)collider.bounds.max.z - 1);

        ray = new Ray(new Vector3(randomX, 10, randomZ), Vector3.down);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.layer == 6) // platform layer
            {
                Instantiate(stackPref, hit.point + new Vector3(0, stackPref.transform.localScale.y / 2, 0), Quaternion.identity);
            }

            /* 
             * Making this function recursive would make the game more "fair"
             * but with a small risk of potential stack overflow
             */
            //else SpawnWithRayCast(stackPref);
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

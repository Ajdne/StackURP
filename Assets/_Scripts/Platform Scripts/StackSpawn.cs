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

    //public List<GameObject> stacksOnPlat = new List<GameObject>();
    private RaycastHit hit;
    private Ray ray;
    private GameManager gm;

    void Start()
    {
        gm = GameManager.Instance;

        collider = GetComponent<MeshCollider>();

        for (int i = 0; i < initialNumberOfStacks; i++)
        {
            // add it to the list
            //stacksOnPlat.Add(gm.SpawnOnRandomPosition(gm.StackPref, this.gameObject));
            SpawnWithRayCast();
            //gm.SpawnOnRandomPosition(gm.StackPref, this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        spawnTime += Time.deltaTime;

        if(spawnTime > stackSpawnTime) // && stacksOnPlat.Count < maxStacks)
        {
            SpawnWithRayCast();
            // add it to the list
            //stacksOnPlat.Add(gm.SpawnOnRandomPosition(gm.StackPref, this.gameObject));

            //gm.SpawnOnRandomPosition(gm.StackPref, this.gameObject);
            //RandomPosition(this.gameObject.GetComponent<Collider>());
            // reset the timer
            spawnTime = 0;
        }
    }

    private void SpawnWithRayCast()
    {
        int randomX = Random.Range((int)collider.bounds.min.x + 2, (int)collider.bounds.max.x - 1);
        int randomZ = Random.Range((int)collider.bounds.min.z + 2, (int)collider.bounds.max.z - 1);
        //int randomZ = Random.Range(-(int)transform.localScale.z, (int)transform.localScale.z);
        ray = new Ray(new Vector3(randomX, 10, randomZ), Vector3.down);

        Debug.DrawRay(new Vector3(randomX, 100, randomZ), Vector3.down, Color.red);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.layer == 6) // platform layer
            {
                GameObject spawn = Instantiate(gm.StackPref, hit.point + new Vector3(0, gm.StackPref.transform.localScale.y /2, 0), Quaternion.identity);
            }
        }
    }

    // ovo moze sa listom kolajdera
    public Vector3 RandomPosition(Collider region)
    {
        //get random Z and X axis values for the random position within the chosen area
        float xRandom = Random.Range(region.bounds.min.x, region.bounds.max.x);
        float zRandom = Random.Range(region.bounds.min.z, region.bounds.max.z);

        Vector3 randomSpawnPosition = new Vector3(xRandom, 0.5f, zRandom); //combine the 2 gotten coordinates to get the final position Vector
        Instantiate(gm.StackPref, randomSpawnPosition, Quaternion.identity);
        return randomSpawnPosition;
    }

}

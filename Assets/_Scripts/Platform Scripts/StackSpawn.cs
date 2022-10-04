using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackSpawn : MonoBehaviour
{
    [SerializeField] private int initialNumberOfStacks;
    //[SerializeField] private int maxStacks;

    [SerializeField] private float stackSpawnTime;
    private float spawnTime;

    //public List<GameObject> stacksOnPlat = new List<GameObject>();
    private RaycastHit hit;
    private Ray ray;
    private GameManager gm;

    void Start()
    {
        gm = GameManager.Instance;

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

    //private void FindSpawnPosition()
    //{
    //    while()
    //    {
    //        int randomX = Random.Range(-(int)transform.localScale.x + 10, (int)transform.localScale.x - 10);
    //        int randomZ = Random.Range(-(int)transform.localScale.z + 10, (int)transform.localScale.z - 10);
    //        ray = new Ray(new Vector3(randomX, 10, randomZ), Vector3.down);

    //        if (Physics.Raycast(ray, out hit))
    //        {
    //            if (hit.transform.gameObject.layer == 6) // platform layer
    //            {
    //                break;
    //                //GameObject spawn = Instantiate(gm.StackPref, hit.point, Quaternion.identity);
    //            }
    //            Debug.DrawRay(new Vector3(randomX, 10, randomZ), Vector3.down, Color.red);

    //        }
    //    }
    //    GameObject spawn = Instantiate(gm.StackPref, hit.point, Quaternion.identity);
    //    //return spawnPos;
    //}

    private void SpawnWithRayCast()
    {
        int randomX = Random.Range(-(int)transform.localScale.x, (int)transform.localScale.x);
        int randomZ = Random.Range(-(int)transform.localScale.z, (int)transform.localScale.z);
        ray = new Ray(new Vector3(randomX, 10, transform.position.z + randomZ), Vector3.down);
        print(ray);

        Debug.DrawRay(new Vector3(randomX, 100, randomZ), Vector3.down, Color.red);

        if (Physics.Raycast(ray, out hit))
        {
            //print("etonas");
            //print(hit.transform.gameObject.layer.ToString());
            if (hit.transform.gameObject.layer == 6) // platform layer
            {
                GameObject spawn = Instantiate(gm.StackPref, hit.point + new Vector3(0, gm.StackPref.transform.localScale.y /2, 0), Quaternion.identity);
                print("opet");
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

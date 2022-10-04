using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private static int level;

    [Header("Platform Settings")]
    [SerializeField] private List<GameObject> platforms; // insert platform prefabs here
    [SerializeField] private GameObject finalPlatform;
    [SerializeField] private List<GameObject> crossings; // insert crossing prefabs here
    private int numberOfPlatforms;
    [SerializeField] private int maxNumberOfPlatforms = 4;
    public List<GameObject> Crossings { get { return crossings; } }

    

    [Header("Player Settings")]
    public GameObject Player;
    private Vector3 playerRespawnPosition;
    public Vector3 PlayerRespawnPos { get { return playerRespawnPosition; } set { playerRespawnPosition = value; } }
    private Stacking stackingScript;

    [Header("Stacks")]
    [SerializeField] private GameObject stackPref;
    public GameObject StackPref { get { return stackPref; } }


    // *******************************************************************
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        level++;
        Physics.gravity *= 2;

        stackingScript = Player.GetComponent<Stacking>();

        SpawnRandomPlatforms();
    }
    private void SpawnRandomPlatforms()
    {
        for (int i = 0; i < maxNumberOfPlatforms; i++)
        {
            if(i != maxNumberOfPlatforms - 1)
            {
                Instantiate(GetRandomItem(platforms), new Vector3(0, 0, 53 * numberOfPlatforms), Quaternion.identity);
            }
            else
            {
                Instantiate(finalPlatform, new Vector3(0, 0, 53 * numberOfPlatforms), Quaternion.identity);
            }
            
            numberOfPlatforms++;
        }
    }

    public GameObject GetRandomItem(List<GameObject> itemList)
    {
        int randomVal = Random.Range(0, itemList.Count);

        GameObject randomItem = itemList[randomVal];

        return randomItem;
    }

    public GameObject SpawnOnRandomPosition(GameObject spawnPref, GameObject locationObj)
    {
        // randomize position
        int randomX = Random.Range((int)-locationObj.transform.localScale.x + 1, (int)locationObj.transform.localScale.x - 1);
        int randomZ = Random.Range((int)-locationObj.transform.localScale.z + 1, (int)locationObj.transform.localScale.z - 1);

        // spawn prefab
        GameObject spawn = Instantiate(spawnPref, locationObj.transform.position + new Vector3(randomX, 0.5f, randomZ), Quaternion.identity);

        return spawn;
    }

    public void RespawnPlayer()
    {
        // remove leftover stacks from player
        stackingScript.RemoveFromStack(stackingScript.GetStackCount());

        // need to disable movement script in order to move him
        Player.GetComponent<Movement2>().player.enabled = false;
        Player.transform.position = playerRespawnPosition;
        Player.GetComponent<Movement2>().player.enabled = true;
    }

    public void SetPlayerPosition(Vector3 newPos)
    {
        Player.GetComponent<Movement2>().player.enabled = false;
        Player.transform.position = newPos;
        Player.GetComponent<Movement2>().player.enabled = true;
    }

    public void RevertPlayerControls()
    {
        //Player.GetComponent<ShortCutRun>().enabled = false;
        Player.GetComponent<CapsuleCollider>().enabled = false;

        Player.GetComponent<Movement2>().enabled = false;
        Player.GetComponent<Rigidbody>().isKinematic = true;
    }
}

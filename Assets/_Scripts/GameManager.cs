using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Platform Settings")]
    [SerializeField] private List<GameObject> platforms; // insert platform prefabs here
    [SerializeField] private GameObject finalPlatform;
    [SerializeField] private List<GameObject> crossings; // insert crossing prefabs here
    private int numberOfPlatforms;
    [SerializeField] private int maxNumberOfPlatforms = 4;
    public List<GameObject> Crossings { get { return crossings; } }

    [Header("Camera Settings"), Space]
    [SerializeField] private CinemachineVirtualCamera cineCamera;
    public CinemachineVirtualCamera CineCamera { get { return cineCamera; } }

    [Header("Player Settings")]
    public GameObject Player;
    private Vector3 playerRespawnPosition;
    public Vector3 PlayerRespawnPos { get { return playerRespawnPosition; } set { playerRespawnPosition = value; } }

    [Header("Stacks"), Space(10f)]
    [SerializeField] private List<GameObject> stackPrefs; // = new List<GameObject>();
    public List<GameObject> StackPrefs { get { return stackPrefs; } set { stackPrefs = value;  } }

    // ENDGAME
    private bool isEndGame;
    public bool IsEndGame { get { return isEndGame; } set { isEndGame = value; } }

    // *******************************************************************
    private void Awake()
    {
        Instance = this;

        Application.targetFrameRate = 60;   // this fixes everything
    }

    private void Start()
    {
        Physics.gravity *= 2;

        //playerAnimator = Player.GetComponent<Animator>();

        //SpawnRandomPlatforms();
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

    //public IEnumerator RespawnPlayer(GameObject player)
    //{
    //    // remove leftover stacks from player
    //    player.GetComponent<IStacking>().RemoveAllStacks();

    //    yield return new WaitForSeconds(0.4f);

    //    //stackingScript.RemoveFromStack(stackingScript.GetStackCount());

    //    // need to disable movement script in order to move him
    //    player.GetComponent<IMovement>().DeactivateMovement();

    //    player.transform.rotation = Quaternion.Euler(0, 180, 0);
    //    player.transform.position = player.RespawnPosition;

    //    if (IsEndGame)
    //    {
    //        playerAnimator.SetBool("Idle", false);
    //        playerAnimator.SetBool("Run", false);
    //        playerAnimator.SetBool("Dance", true);

    //        // dont activate player movement
    //        //Player.GetComponent<CapsuleCollider>().enabled = false;
    //        Player.GetComponent<Rigidbody>().isKinematic = true;
    //    }
    //    else player.GetComponent<IMovement>().ActivateMovement();

    //}

    //public void SetPlayerPosition(Vector3 newPos)
    //{
    //    Player.GetComponent<Movement2>().player.enabled = false;
    //    Player.transform.position = newPos;
    //    Player.GetComponent<Movement2>().player.enabled = true;
    //}

    public void RevertPlayerControls(GameObject player)
    {
        //Player.GetComponent<ShortCutRun>().enabled = false;
        //player.GetComponent<CapsuleCollider>().enabled = false;

        player.GetComponent<IMovement>().DeactivateMovement();
        //player.GetComponent<IMovement>().DeactivateMovement();
        player.GetComponent<Rigidbody>().isKinematic = true;
    }

    public GameObject GetObjectFromList(int i)
    {
        return stackPrefs[i];
    }
}

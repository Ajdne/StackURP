using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Space(5f), Header("Platform Settings"), Space(2f)]
    [SerializeField] private List<GameObject> platforms; // insert platform prefabs here
    [SerializeField] private GameObject finalPlatform;
    [SerializeField] private List<GameObject> crossings; // insert crossing prefabs here
    private int numberOfPlatforms;
    [SerializeField] private int maxNumberOfPlatforms = 4;
    public List<GameObject> Crossings { get { return crossings; } }

    #region Camera Settings
    [Space(5f), Header("Camera Settings"), Space(2f)]
    [SerializeField] private CinemachineVirtualCamera cineCamera;
    public CinemachineVirtualCamera CineCamera { get { return cineCamera; } }
    #endregion

    [Space(5f), Header("Player Settings"), Space(2f)]
    public GameObject Player;
    private Vector3 playerRespawnPosition;
    public Vector3 PlayerRespawnPos { get { return playerRespawnPosition; } set { playerRespawnPosition = value; } }

    [Space(5f), Header("AI Settings"), Space(2f)]
    [SerializeField] private List<GameObject> bots = new List<GameObject>();
    public List<GameObject> Bots { get { return bots; } }
    
    [Space(5f), Header("Stacks"), Space(10f)]
    [SerializeField] private List<GameObject> stackPrefs; // = new List<GameObject>();
    public List<GameObject> StackPrefs { get { return stackPrefs; } set { stackPrefs = value;  } }

    // ENDGAME
    private bool isEndGame;
    public bool IsEndGame { get { return isEndGame; } set { isEndGame = value; } }

    //[Header("Time Settings"), Space(5f)]
    //[SerializeField] private float testingScaleTime = 1;

    // *******************************************************************
    private void Awake()
    {
        Instance = this;

        Application.targetFrameRate = 60;   // this fixes everything
    }

    private void Start()
    {
        Physics.gravity *= 2;

        //Time.timeScale = testingScaleTime;

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

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MultiplierPlatform : MonoBehaviour
{
    [SerializeField] private float speed;
    private int multiplierValue;
    [SerializeField] private TextMeshProUGUI multiplierValueCanvas;
    [SerializeField] private GameObject particle;
    [SerializeField] private Animator popAnimator;
    [SerializeField] private AudioSource audioSource;

    private IStacking playerStacking;
    private IMovement playerMovementScript;

    private bool isTriggered;
    private bool isLastPlatform;
    public bool IsLastPlatform { set { isLastPlatform = value; } }

    private void Start()
    {
        playerMovementScript = GameManager.Instance.Player.GetComponent<IMovement>();
        playerStacking = GameManager.Instance.Player.GetComponent<IStacking>();
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, 0.5f, transform.position.z), speed * Time.deltaTime);

        if(transform.position.y == 0.5f)
        {
            // POP!
            particle.SetActive(true);
            popAnimator.enabled = true;
        }
    }

    // THIS IS FOR LEVEL END PLATFORMS
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10 && !isTriggered)
        {
            // POP!
            particle.SetActive(false);
            popAnimator.enabled = false;
            particle.SetActive(true);
            popAnimator.enabled = true;

            // play audio
            audioSource.pitch = 0.5f + multiplierValue * 0.1f;
            audioSource.Play();

            // increase player speed by (value)
            playerMovementScript.IncreaseMovementSpeed(2f);

            // save player location
            playerMovementScript.SetPlayerRespawnPosition(transform.position + new Vector3(0, 1, 0));  

            // if the player has no stacks left or the platform reached is the last platform, end the game level
            if ( playerStacking.GetStackCount() <= 7 || isLastPlatform)
            {
                // set the player pn the middle of the platform
                StartCoroutine(playerMovementScript.RespawnPlayer());

                // turn off all bots
                for (int i = 0; i < GameManager.Instance.Bots.Count; i++)
                {
                    GameManager.Instance.Bots[i].SetActive(false);
                }
                
            }

            isTriggered = true;
        }
    }

    public void SetMultiplierValue(int value)
    {
        multiplierValue = value;

        // update canvas
        multiplierValueCanvas.text = "x" + multiplierValue.ToString();
    }

}

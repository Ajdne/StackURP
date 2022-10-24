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

    private float playerSpeed;
    private Stacking playerStacking;

    private Movement2 playerMovmentScript;

    private bool isTriggered;
    private bool isLastPlatform;
    public bool IsLastPlatform { set { isLastPlatform = value; } }

    private void Start()
    {
        playerMovmentScript = GameManager.Instance.Player.GetComponent<Movement2>();
        playerSpeed = playerMovmentScript.MoveSpeed;
        playerStacking = GameManager.Instance.Player.GetComponent<Stacking>();
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

            // increase player speed
            playerSpeed += 1f;

            // save player location
            other.gameObject.GetComponent<IMovement>().SetPlayerRespawnPosition(transform.position + new Vector3(0, 1, 0));  

            // if the player has no stacks left or the platform reached is the last platform, end the game level
            if ( playerStacking.GetStackCount() <= 10 || isLastPlatform)
            {
                Debug.Log("Stacks left:" + playerStacking.GetStackCount());
                Debug.Log(isLastPlatform);
                // set the player pn the middle of the platform
                other.gameObject.GetComponent<IMovement>().RespawnPlayer();
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

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

    private bool isTriggered;
    private bool isLastPlatform;
    public bool IsLastPlatform { set { isLastPlatform = value; } }

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3 && !isTriggered)
        {
            // POP!
            particle.SetActive(false);
            popAnimator.enabled = false;
            particle.SetActive(true);
            popAnimator.enabled = true;

            // increase player speed
            GameManager.Instance.Player.GetComponent<Movement2>().MoveSpeed += 1f;

            // save player location
            GameManager.Instance.PlayerRespawnPos = transform.position + new Vector3(0, 1, 0);

            // if the player has no stacks left or the platform reached is the last platform, end the game level
            if (other.gameObject.GetComponent<Stacking>().GetStackCount() == 0 || isLastPlatform)
            {
                StartCoroutine(GameManager.Instance.RespawnPlayer());
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

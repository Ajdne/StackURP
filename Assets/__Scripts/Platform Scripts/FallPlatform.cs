using Lofelt.NiceVibrations;
using System.Collections;
using UnityEngine;

public class FallPlatform : MonoBehaviour
{
    [SerializeField] private GameObject waterSplashParticle;
    private bool isTriggered;

    [Header("Audio Settings")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip waterSplashClip;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTriggered)
        {
            isTriggered = true;

            // spawn splash particle
            Instantiate(waterSplashParticle, other.transform.position, Quaternion.identity);

            // change player position
            StartCoroutine(other.gameObject.GetComponent<IMovement>().RespawnPlayer());

            StartCoroutine(ResetBool());

            // vibrate and play water splash sound
            if (other.gameObject.layer == 10)
            {
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.Warning);
                audioSource.PlayOneShot(waterSplashClip);
            }
        }
        else if (other.gameObject.layer == 0)
        {
            // using this to optimise performance
            // deactivate the object - used for neutral bricks after collision
            StartCoroutine(DeactivateObject(other.gameObject));
        }
    }

    private IEnumerator ResetBool()
    {
        yield return new WaitForSeconds(0.2f);
        isTriggered = false;
    }

    private IEnumerator DeactivateObject(GameObject obj)
    {
        yield return new WaitForSeconds(1);
        obj.SetActive(false);
    }
}


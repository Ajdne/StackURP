using Lofelt.NiceVibrations;
using System.Collections;
using System.Collections.Generic;
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
            StartCoroutine(GameManager.Instance.RespawnPlayer(other.gameObject));

            //GameManager.Instance.RespawnPlayer(other.gameObject);

            //GameManager.Instance.Invoke("RespawnPlayer", 0.5f);

            StartCoroutine(ResetBool());

            // vibrate and play water splash sound
            if (other.gameObject.layer == 10)
            {
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.Warning);
                audioSource.PlayOneShot(waterSplashClip);
            }
        }
    }

    private IEnumerator ResetBool()
    {
        yield return new WaitForSeconds(0.5f);
        isTriggered = false;
    }
}


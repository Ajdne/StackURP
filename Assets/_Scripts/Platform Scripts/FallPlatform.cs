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
        if (other.gameObject.layer == 3) // player layer
        {
            // play water splash audio
            if(!isTriggered)
            {
                // spawn splash particle
                Instantiate(waterSplashParticle, other.transform.position, Quaternion.identity);

                audioSource.PlayOneShot(waterSplashClip);
                isTriggered = true;
            }

            // change player position
            StartCoroutine(GameManager.Instance.RespawnPlayer());
            
            //GameManager.Instance.Invoke("RespawnPlayer", 0.5f);

            StartCoroutine(ResetBool());
        }
    }

    private IEnumerator ResetBool()
    {
        yield return new WaitForSeconds(0.2f);
        isTriggered = false;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPlatform : MonoBehaviour
{
    [SerializeField] private GameObject waterSplashParticle;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3) // player layer
        {
            // spawn splash particle
            Instantiate(waterSplashParticle, other.transform.position, Quaternion.identity);

            // change player position
            GameManager.Instance.RespawnPlayer();
        }
    }
}

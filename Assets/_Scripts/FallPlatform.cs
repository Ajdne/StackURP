using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FallPlatform : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3) // player layer
        {
            print("ALOBRE");
            SceneManager.LoadScene(0);
        }
    }
}

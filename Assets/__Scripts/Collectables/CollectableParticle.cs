using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableParticle : MonoBehaviour
{
    [SerializeField] private GameObject stackParticles;

    public void ActivateStackParticle()
    {
        if(stackParticles != null) stackParticles.SetActive(true);
    }
}

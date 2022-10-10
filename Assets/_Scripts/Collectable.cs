using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    //[SerializeField] private int layer;
    [SerializeField] private GameObject stackParticles;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.gameObject.layer == this.gameObject.layer) // player layer
        {
            //StartCoroutine(other.GetComponent<Stacking>().MoveToStack(this.gameObject));
            other.GetComponent<Stacking>().AddMoneyToStack(this.gameObject);
        }
    }

    public void ActivateStackParticle()
    {
        stackParticles.SetActive(true);
    }
}

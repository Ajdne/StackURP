using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollectable : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.gameObject.layer == 10) // player layer
        {
            other.GetComponent<Stacking>().AddMoneyToStack(this.gameObject);
        }
    }
}

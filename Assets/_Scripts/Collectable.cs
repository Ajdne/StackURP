using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // if its an AI and matches his layer
        if (other.CompareTag("Player") && other.gameObject.layer == this.gameObject.layer)
        {
            other.GetComponent<EnemyStacking>().AddMoneyToStack(this.gameObject);            
        }
    }
}

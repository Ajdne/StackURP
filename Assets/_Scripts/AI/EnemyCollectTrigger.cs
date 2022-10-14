using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollectTrigger : MonoBehaviour
{
    private CollectingState movement;
    private EnemyAI playerAI;

    private void Start()
    {
        movement = GetComponentInParent<CollectingState>();
        playerAI = GetComponentInParent<EnemyAI>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == playerAI.gameObject.layer)
        {
            if(!movement.CollectList.Contains(other.gameObject.transform))
            {
                // add collectable to list
                movement.CollectList.Add(other.transform);
            }
        }
    }

}

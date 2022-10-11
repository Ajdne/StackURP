using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollectTrigger : MonoBehaviour
{
    private EnemyAI enemyAI;

    private void Start()
    {
        enemyAI = GetComponentInParent<EnemyAI>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == this.gameObject.layer)
        {
            // add collectable to list
            enemyAI.CollectList.Add(other.transform);
        }
    }

}

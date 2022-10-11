using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private int lookRadius;
    private List<Transform> collectList = new List<Transform>();
    public List<Transform> CollectList { get { return collectList; } set { collectList = value; } }

    [Space]
    [SerializeField] private int stacksToCollect;
    

    private enum possibleStates
    {
        COLLECTING,
        UNLOADING,
        SHORTCUT
    }
    private possibleStates state;

    void Start()
    {
        state = possibleStates.COLLECTING;
    }

    void Update()
    {
        // ako skuplja, ide po listi redom, dok ne dodje do odredjenog broja
        if(state == possibleStates.COLLECTING)
        {
            
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}

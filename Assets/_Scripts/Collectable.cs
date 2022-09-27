using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private List<GameObject> moneys = new List<GameObject>();
    public List<GameObject> Moneys { get { return moneys; } set { moneys = value; } }

    // [SerializeField] private GameObject unlockParticles;

    private IEnumerator stackCoroutine;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3) // player layer
        {
            //StartCoroutine(other.GetComponent<Stacking>().MoveToStack(this.gameObject));
            other.GetComponent<Stacking>().AddMoneyToStack(this.gameObject);
        }
    }
}

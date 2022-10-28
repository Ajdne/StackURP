using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GiveCrown : MonoBehaviour
{
    [Header("Crown Settings"), Space(5f)]
    [SerializeField] private GameObject crownPrefab;
    [SerializeField] private Animator crownAnimator;
    [SerializeField] private ParticleSystem crownParticles;

    [Space]
    [SerializeField] private List<GameObject> playerHeads = new List<GameObject>();
    public List<GameObject> PlayerHeads { get { return playerHeads; } }

    [SerializeField] private int checkTimer;

    private float timer;

    private void Start()
    {
        crownAnimator = crownPrefab.GetComponent<Animator>();
        
    }

    void Update()
    {
        timer += Time.deltaTime;

        //var emission1 = crownParticles.emission;

        if(timer > checkTimer)
        {
            GameObject closestToFinishLine = CompareDistance(CompareDistance(playerHeads[0], playerHeads[1]), CompareDistance(playerHeads[2], playerHeads[3]));
            //emission1.enabled = false;

            // need to check this condition to prevent animation poping every time
            if (crownPrefab.transform.parent != closestToFinishLine.transform)
            {
                GiveCrownToPlayer(closestToFinishLine);
            }

            // reset the timer
            timer = 0;
        }
    }

    GameObject CompareDistance(GameObject obj1, GameObject obj2)
    {
        float distance1 = Vector3.Distance(transform.position, obj1.transform.position);
        float distance2 = Vector3.Distance(transform.position, obj2.transform.position);

        if(distance1 <= distance2)
        {
            return obj1;
        }
        else
        {
            return obj2;
        }
    }

    public void GiveCrownToPlayer(GameObject playerHead)
    {
        crownPrefab.transform.parent = playerHead.transform;
        crownPrefab.transform.position = playerHead.transform.position;
        crownPrefab.transform.rotation = playerHead.transform.rotation;

        // play crown pop animation
        crownAnimator.Play("CrownPop");

        // play particles
        crownParticles.Play();
    }
}

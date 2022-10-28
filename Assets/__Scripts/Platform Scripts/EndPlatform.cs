using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPlatform : MonoBehaviour
{
    [SerializeField] private List<GameObject> endPlatforms;
    [SerializeField] private List<GameObject> endPlatformMultipliers;
    [SerializeField] private GameObject multiplierPlatformPrefab;
    [SerializeField] private int numberOfEndPlatforms;
    [SerializeField] private GameObject particle;

    [Header("Crown")]
    [SerializeField] private GiveCrown crownScript;
    
    [Space]
    [SerializeField] private Animator camAnimator;

    //private bool isFirst = true;
    private bool isTriggered = false;
    private GameObject isFirst;

    bool isFirstTriggerd;

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // change animation
            if (isFirst == null && !isFirstTriggerd )
            {
                isFirstTriggerd = true;

                isFirst = other.gameObject;

                if(other.gameObject.layer == 10 && !isTriggered) // blue player layer
                {
                    Debug.Log("PRVI");
                    // save player location
                    other.GetComponent<IMovement>().SetPlayerRespawnPosition(transform.position + new Vector3(0, 1, 0));
                    //GameManager.Instance.PlayerRespawnPos = transform.position + new Vector3(0, 1, 0);

                    // camera transition by changing vcam priority
                    GameManager.Instance.CineCamera.Priority += 2;

                    StartCoroutine(EndPlatformActivate());

                    // activate particle
                    //particle.SetActive(true);

                    isTriggered = true;
                    

                    other.GetComponent<Movement2>().MoveSpeed *= 1.1f;

                    // activate BOOL for dance animation
                    GameManager.Instance.IsEndGame = true;

                }

                // then its a bot
                else
                {
                    Debug.Log("BOT JE PRIV");
                    // turn off bot movement
                    other.GetComponent<IMovement>().DeactivateMovement();

                    // dance animation
                    //other.GetComponent<Animator>().Play("Dance");
                }

                // turn of crown holder change
                crownScript.enabled = false;
               // isFirst = false;
            }
            // then he is not first
            else
            {
                Debug.Log("NIJE PRVI");
                // turn off movement
                other.GetComponent<IMovement>().DeactivateMovement();

                // sad animation
                //other.GetComponent<Animator>().Play("Sad");
            }
        }
    }
   
    IEnumerator EndPlatformActivate()
    {
        for (int i = 0; i < endPlatforms.Count; i++)
        {
            int xRange = endPlatforms.Count;
            int randomX = Random.Range(-xRange, xRange);
            yield return new WaitForSeconds(0.5f);

            // set platform prefab position
            endPlatforms[i].transform.position = new Vector3(transform.position.x + randomX, -20, transform.position.z + (1 + i) * (15 + i));

            // activate platform prefabs
            endPlatforms[i].SetActive(true);

            //GameObject multiPlat = Instantiate(multiplierPlatformPrefab, new Vector3(transform.position.x + randomX, -20, transform.position.z + (1 + i) * (15 + i)), Quaternion.identity);
            //endPlatformMultipliers.Add(multiPlat);

            // set multiplier value for bonus platforms
            endPlatforms[i].GetComponent<MultiplierPlatform>().SetMultiplierValue(i + 1);


            if (i == endPlatforms.Count - 1)
            {
                endPlatforms[i].GetComponent<MultiplierPlatform>().IsLastPlatform = true;
            }
            // scaling
            //float platSize = multiPlat.transform.localScale.x + i * 0.5f;
            //multiPlat.transform.localScale = new Vector3(platSize, 1, platSize);


            //stackScript.TargetGroup.AddMember(multiPlat.transform, 30, 5f);

            yield return new WaitForSeconds(0.25f);
        }
    }
}
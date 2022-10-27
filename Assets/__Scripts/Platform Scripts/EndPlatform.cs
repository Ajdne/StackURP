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
    
    [Space]
    [SerializeField] private Animator camAnimator;

    private bool isTriggered;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 10 && !isTriggered)
        {
            // save player location
            other.gameObject.GetComponent<IMovement>().SetPlayerRespawnPosition(transform.position + new Vector3(0, 1, 0));
            //GameManager.Instance.PlayerRespawnPos = transform.position + new Vector3(0, 1, 0);

            // camera transition by changing vcam priority
            GameManager.Instance.CineCamera.Priority += 2;

            StartCoroutine(EndPlatformActivate());

            // activate particle
            //particle.SetActive(true);

            isTriggered = true;

            GameManager.Instance.Player.GetComponent<Movement2>().MoveSpeed *= 1.1f;

            // activate BOOL for dance animation
            GameManager.Instance.IsEndGame = true;

            GetComponent<GiveCrown>().enabled = false;
        }
    }
   
    IEnumerator EndPlatformActivate()
    {
        for (int i = 0; i < endPlatforms.Count; i++)
        {
            int xRange = (int)(endPlatforms.Count * 1.2f);
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

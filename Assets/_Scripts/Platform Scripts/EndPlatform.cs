using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPlatform : MonoBehaviour
{
    [SerializeField] private List<GameObject> endPlatformMultipliers;
    [SerializeField] private GameObject multiplierPlatformPrefab;
    [SerializeField] private int numberOfEndPlatforms;

    private bool isTriggered;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3 && !isTriggered)
        {
            StartCoroutine(EndPlatformSpawn());
            isTriggered = true;

            GameManager.Instance.Player.GetComponent<Movement2>().MoveSpeed *= 1.1f;

            // activate BOOL for dance animation
            GameManager.Instance.IsEndGame = true;
        }
    }
   
    IEnumerator EndPlatformSpawn()
    {
        for (int i = 0; i < numberOfEndPlatforms; i++)
        {
            int xRange = 6 + endPlatformMultipliers.Count * 2;
            int randomX = Random.Range(-xRange, xRange + 1);
            yield return new WaitForSeconds(0.5f);

            // spawn platform prefab
            GameObject multiPlat = Instantiate(multiplierPlatformPrefab, new Vector3(transform.position.x + randomX, -20, transform.position.z + (1 + i) * (15 + i)), Quaternion.identity);
            endPlatformMultipliers.Add(multiPlat);

            // set multiplier value for bonus platforms
            multiPlat.GetComponent<MultiplierPlatform>().SetMultiplierValue(i + 1);

            // scaling
            float platSize = multiPlat.transform.localScale.x + endPlatformMultipliers.Count - 1;
            multiPlat.transform.localScale = new Vector3(platSize, 1, platSize);

            //stackScript.TargetGroup.AddMember(multiPlat.transform, 30, 5f);

            yield return new WaitForSeconds(0.25f);
        }
    }
}

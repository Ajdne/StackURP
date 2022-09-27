using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PropertyZone))]
public class MoneySpawn : MonoBehaviour
{
    [SerializeField] private GameObject moneyPrefab;

    private List<GameObject> moneysFromSpawn;

    private Stacking stackingScript;

    private float timer;
    private float spawnTime;

    private float stayTimer;
    [SerializeField] private float stayToCollectTime;

    [SerializeField] private int maxNumberOfMoney;

    private void Start()
    {
        //moneysFromSpawn = GetComponent<PropertyZone>().Moneys;

        // sec/dollars
        //spawnTime = 100 / GetComponent<PropertyZone>().DollarsPerSecond;    
    }

    void Update()
    {
        timer += Time.deltaTime;

        // if there is already enough food, then don't do anything
        //if(foods.Count >= maxNumberOfFood) return;

        if (timer >= spawnTime && moneysFromSpawn.Count < maxNumberOfMoney) // * TimerFactor);
        {
            // local variable
            GameObject moneyToList = Instantiate(moneyPrefab, transform.position + new Vector3(0, 2, 0), Quaternion.Euler(-90, 0, 0));
            
            // add the money to list
            moneysFromSpawn.Add(moneyToList);

            // reset the timer
            timer = 0;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Multipliers : MonoBehaviour
{
    private bool activated;
    private int randomValue;
    private int randomEffect;

    [SerializeField] private bool isPositiveMultiplier;
    [SerializeField] private TextMeshProUGUI multiplierValue;
    [SerializeField] private GameObject particle;

    private void Start()
    {
        GetRandomMultiplierEffect();
    }

    private void GetRandomMultiplierEffect()
    {
        // randomize multiplier effect (+, -, *, /)
        if(isPositiveMultiplier)
        {
            randomEffect = 2;
        }
        else
        {
            randomEffect = Random.Range(0, 4);
        }

        switch (randomEffect)
        {
            case 0:
                // add
                randomValue = Random.Range(4, 11);
                multiplierValue.text = "+" + randomValue.ToString();
                break;
            case 1:
                // subtract
                randomValue = Random.Range(2, 7);
                multiplierValue.text = "-" + randomValue.ToString();
                break;
            case 2:
                // multiply
                randomValue = Random.Range(2, 5);
                multiplierValue.text = "x" + randomValue.ToString();
                break;
            case 3:
                // divide
                randomValue = Random.Range(2, 4);
                multiplierValue.text = "/" + randomValue.ToString();
                break;
        }
    }

    private void DoRandomEffect(int multiplierValue, GameObject player)
    {
        switch (randomEffect)
        {
            case 0:
                // add
                player.GetComponent<Stacking>().InstantiateToStack(multiplierValue);
                break;
            case 1:
                // subtract
                player.GetComponent<Stacking>().RemoveFromStack(multiplierValue);
                break;
            case 2:
                // multiply
                player.GetComponent<Stacking>().MultiplyStack(multiplierValue);
                break;
            case 3:
                // divide
                player.GetComponent<Stacking>().DivideStack(multiplierValue);
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3) // player layer
        {
            DoRandomEffect(randomValue, other.gameObject);

            // play particle effect
            particle.SetActive(true);

            // disable collider
            GetComponent<BoxCollider>().enabled = false;

            // disable canvas
            multiplierValue.enabled = false;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Multipliers : MonoBehaviour
{
    [Header("Does it has a positive effect?")]
    [SerializeField] private bool isPositiveMultiplier;
    
    [Header("Connected components"), Space(10f)]
    [SerializeField] private TextMeshProUGUI multiplierValue;
    [SerializeField] private GameObject particle;
    [SerializeField] private Animator animator;

    private int randomValue;
    private int randomEffect;

    // using this to trigger the effect only once
    private bool isTriggered;


    private delegate void delegateMultiplierEffect(GameObject obj);
    delegateMultiplierEffect passedMethod;

    private void Start()
    {
        GetRandomMultiplierEffect();
    }
    private void GetRandomMultiplierEffect()
    {
        // randomize multiplier effect (+, *, -, /)
        if(isPositiveMultiplier)
        {
            randomEffect = UnityEngine.Random.Range(0, 2);
        }
        else
        {
            randomEffect = UnityEngine.Random.Range(0, 4);
        }

        switch (randomEffect)
        {
            case 0:
                // add
                // randomise value of the effect
                randomValue = UnityEngine.Random.Range(4, 11);
                multiplierValue.text = "+" + randomValue.ToString();

                // pass the method here
                passedMethod = PlusMultiplier;
                break;

            case 1:
                // multiply
                randomValue = UnityEngine.Random.Range(2, 3);
                multiplierValue.text = "x" + randomValue.ToString();

                // pass the method here
                passedMethod = MultiplyMultiplier;
                break;
            case 2:
                // subtract
                randomValue = UnityEngine.Random.Range(2, 7);
                multiplierValue.text = "-" + randomValue.ToString();

                // pass the method here
                passedMethod = SubtractMultiplier;
                break;
            case 3:
                // divide
                randomValue = UnityEngine.Random.Range(2, 4);
                multiplierValue.text = "/" + randomValue.ToString();

                // pass the method here
                passedMethod = DivideMultiplier;
                break;
        }
    }

    #region Mulitiplier Efects
    private void PlusMultiplier(GameObject other)
    {
        other.GetComponent<IStacking>().InstantiateToStack(randomValue);
    }
    private void MultiplyMultiplier(GameObject other)
    {
        other.GetComponent<IStacking>().MultiplyStack(randomValue);
    }
    private void SubtractMultiplier(GameObject other)
    {
        other.GetComponent<IStacking>().RemoveFromStack(randomValue);
    }
    private void DivideMultiplier(GameObject other)
    {
        other.GetComponent<IStacking>().DivideStack(randomValue);
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !isTriggered)
        {
            // safety bool
            isTriggered = true;

            // use the passed method here
            passedMethod(other.gameObject);

            // play particle effect
            particle.SetActive(true);

            // play animation
            animator.enabled = true;

            // and destroy this element
            // this method should be called in the animation
        }
    }

    private void DestroyThisObject()    // this is called as the animation event
    {
        Destroy(this.gameObject);
    }
}
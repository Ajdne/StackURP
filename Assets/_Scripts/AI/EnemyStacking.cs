using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStacking : MonoBehaviour, IStacking
{
    [SerializeField] private GameObject backpackObj;
    [SerializeField] private int stackFlySpeed;

    private GameObject stackPref;

    public Action<GameObject> collectStack;

    private List<GameObject> stacked = new List<GameObject>();

    private void Start()
    {
        // take the stack prefab from game manager list that coresponds to player layer - 10
        stackPref = GameManager.Instance.StackPrefs[this.gameObject.layer - 10];
    }


    public void AddMoneyToStack(GameObject moneyObj)
    {
        // add obj to list
        stacked.Add(moneyObj.gameObject);

        // reset the collectables layer to prevent them from triggering AI 
        moneyObj.gameObject.layer = 0;

        // set parent
        moneyObj.gameObject.transform.SetParent(backpackObj.transform);

        // set local position
        moneyObj.gameObject.transform.localPosition = backpackObj.transform.localPosition + new Vector3(0, stacked.Count * 0.4f, 0); //moneyObj.transform.localScale.y
        //potrebne su mi normalizovane vrednosti

        

        // set local rotation
        moneyObj.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);

        // ACTION
        collectStack?.Invoke(moneyObj);

        // remove rigidbody
        Destroy(moneyObj.gameObject.GetComponent<Rigidbody>());
        //moneyObj.GetComponent<Rigidbody>().isKinematic = true;

        // disable colliders
        moneyObj.GetComponent<BoxCollider>().enabled = false;

        // play stack particle
        moneyObj.GetComponent<CollectableParticle>().ActivateStackParticle();
    }

    public void InstantiateToStack(int addNumberOfStacks)    // spawn blocks in the stack (for multipliers)
    {
        for (int i = 0; i < addNumberOfStacks; i++)
        {
            GameObject stackObj = Instantiate(stackPref);

            AddMoneyToStack(stackObj);
        }
    }

    public void MultiplyStack(int multiplierCoefficient)
    {
        // calculate the number of stacks to add
        // = current stack count * multiplier coefficient - current stack amount
        int addThisAmountOfStacks = GetStackCount() * multiplierCoefficient - GetStackCount();

        for (int i = 0; i < addThisAmountOfStacks; i++)
        {
            GameObject stackObj = Instantiate(stackPref);

            AddMoneyToStack(stackObj);
        }
    }

    public void RemoveFromStack(int subtractNumberOfStacks)    // spawn blocks in the stack (for multipliers)
    {
        for (int i = 0; i < subtractNumberOfStacks; i++)
        {
            // if the stack is empty
            if (stacked.Count == 0) return;

            // local obj
            GameObject moneyobj = stacked[stacked.Count - 1];

            // remove it from list
            stacked.Remove(moneyobj);
            // and destroy
            Destroy(moneyobj);
        }
    }

    public void DivideStack(int divideTheStackBy)
    {
        float removeThisAmountOfStacks = GetStackCount() - Mathf.Round((float)GetStackCount() / (float)divideTheStackBy);

        for (int i = 0; i < removeThisAmountOfStacks; i++)
        {
            // if the stack is empty
            if (stacked.Count == 0) return;

            // local obj
            GameObject moneyobj = stacked[stacked.Count - 1];

            // remove it from list
            stacked.Remove(moneyobj);
            // and destroy
            Destroy(moneyobj);
        }
    }

    public void RemoveMoneyToProperty(Vector3 objPos, bool destroy)
    {
        // local obj
        GameObject moneyObj = stacked[stacked.Count - 1];

        //remove animator from stackables
        Destroy(moneyObj.GetComponent<Animator>());

        // remove parent
        moneyObj.gameObject.transform.SetParent(null);

        // activate move component
        moneyObj.GetComponent<MoneyMovement>().SetMoveToVector(objPos, destroy);

        // change the stacked travel speed based on the number of stacks
        moneyObj.GetComponent<MoneyMovement>().Speed += stacked.Count * 0.2f;

        // enable the script
        moneyObj.GetComponent<MoneyMovement>().enabled = true;

        // remove it from list
        stacked.Remove(moneyObj);
    }

    public void RemoveStackToShortcut(Vector3 objPos)
    {
        // local obj
        GameObject moneyObj = stacked[stacked.Count - 1];

        // remove parent
        moneyObj.gameObject.transform.SetParent(null);

        //remove animator from stackables
        Destroy(moneyObj.GetComponent<Animator>());

        // remove it from list
        stacked.Remove(moneyObj);

        // set position
        moneyObj.transform.position = objPos;
        // scale up
        moneyObj.transform.localScale *= 1.5f;

        //set rotation
        moneyObj.transform.rotation = Quaternion.Euler(transform.localEulerAngles);

        // add box collider because enabling it will enable the trigger one
        BoxCollider collider = moneyObj.AddComponent<BoxCollider>();
        collider.size = new Vector3(2f, 0.7f, 1);
    }

    public void RemoveAllStacks()
    {
        for (int i = 0; i < stacked.Count; i++)
        {
            // if the stack is empty
            if (stacked.Count == 0) return;

            // local obj
            GameObject stackObj = stacked[i];

            // remove it from list
            //stacked.Remove(stackObj);

            // and destroy
            Destroy(stackObj);
        }

        // clear the list, but delete all objects before that
        stacked.Clear();
    }

    public int GetStackCount()
    {
        return stacked.Count;
    }

    public void LoseStacks()
    {
        for (int i = 0; i < stacked.Count; i++)
        {
            // if the stack is empty
            if (stacked.Count == 0) return;

            // local obj
            GameObject stackObj = stacked[i];

            // add colliders, rigidbody, change collor etc
            stackObj.transform.parent = null;
            //stackObj.GetComponentInChildren<BoxCollider>().enabled = true;
            stackObj.AddComponent<Rigidbody>();

            stackObj.AddComponent<BoxCollider>();
            //stackObj.layer = 0;

            // activate neutral material script
            
            stackObj.AddComponent<NeutralCollectable>().enabled = true;
            //stackObj.GetComponent<NeutralCollectable>().SetMaterialToNeutral();

            // remove it from list
            //stacked.Remove(stackObj);

            // and destroy
            //Destroy(stackObj);
        }

        // clear the list, but delete all objects before that
        stacked.Clear();
    }
}

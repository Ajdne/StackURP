using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Lofelt.NiceVibrations;

public class Stacking : MonoBehaviour , IStacking
{
    [SerializeField] private GameObject backpackObj;
    [SerializeField] private int stackFlySpeed;

    private GameObject stackPref;

    private List<GameObject> stacked = new List<GameObject>();
    public List<GameObject> Stacked { get { return stacked; } }

    [Header("Camera Settings")]
    [SerializeField] private CinemachineTargetGroup cineCameraTargetGroup;
    public CinemachineTargetGroup TargetGroup { get { return cineCameraTargetGroup; } set { cineCameraTargetGroup = value; } }
    [SerializeField] private int maxSizeOfTargetGroup;

    [Header("Audio Settings")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip stackClip;
    [SerializeField] private AudioClip payClip;

    private void Start()
    {
        // take the stack prefab from game manager list that coresponds to player layer - 10
        stackPref = GameManager.Instance.StackPrefs[this.gameObject.layer - 10];

        // set the 1st target to be the player
        if (cineCameraTargetGroup.IsEmpty)
        {
            cineCameraTargetGroup.AddMember(this.gameObject.transform, 40, 5); // AddMember(Transform t, float weight, float radius)
        }
    }


// ----------------- CHEAT FOR TESTING ---------------
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            InstantiateToStack(40);
        }
    }
// ----------------- ---------------- ---------------

    public void AddMoneyToStack(GameObject moneyObj)
    {
        // add obj to list
        stacked.Add(moneyObj.gameObject);

        // set parent
        moneyObj.gameObject.transform.SetParent(backpackObj.transform);

        // set local position
        moneyObj.gameObject.transform.localPosition = backpackObj.transform.localPosition + new Vector3(0, stacked.Count * 0.4f, 0); //moneyObj.transform.localScale.y
        //potrebne su mi normalizovane vrednosti

        // set local rotation
        moneyObj.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);

        // remove rigidbody
        Destroy(moneyObj.gameObject.GetComponent<Rigidbody>());
        //moneyObj.GetComponent<Rigidbody>().isKinematic = true;

        // disable colliders
        moneyObj.GetComponent<BoxCollider>().enabled = false;

        if(stacked.Count < maxSizeOfTargetGroup)
        {
            // set the 2nd target of the camera to the last added stacked object
            cineCameraTargetGroup.AddMember(moneyObj.transform, 3, 1f);
        }

        // play stack particle
        moneyObj.GetComponent<CollectableParticle>().ActivateStackParticle();

        // ---------- STACK AUDIO ------------
        PlayStackAudio();

        HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);
    }

    private void PlayStackAudio()
    {
        audioSource.clip = stackClip;
        audioSource.pitch = 0.5f + stacked.Count * 0.02f;
        audioSource.Play();
    }

    public void InstantiateToStack(int addNumberOfStacks)    // spawn blocks in the stack (for multipliers)
    {
        for(int i = 0; i < addNumberOfStacks; i++)
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

        HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);

    }

    public void RemoveFromStack(int subtractNumberOfStacks)    // spawn blocks in the stack (for multipliers)
    {
        for (int i = 0; i < subtractNumberOfStacks; i++)
        {
            // if the stack is empty
            if (stacked.Count == 0) return;

            // local obj
            GameObject moneyobj = stacked[stacked.Count - 1];

            if (stacked.Count <= maxSizeOfTargetGroup)
            {
                // remove the object from cinemachine target group
                cineCameraTargetGroup.RemoveMember(moneyobj.transform);
            }

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

    public void LoseStacks()
    {
        for (int i = stacked.Count - 1; i > -1; i--)
        {
            // local obj
            GameObject stackObj = stacked[i];

            // add colliders, rigidbody, change collor etc
            stackObj.transform.parent = null;
            //stackObj.GetComponentInChildren<BoxCollider>().enabled = true;
            stackObj.AddComponent<Rigidbody>();

            stackObj.GetComponent<BoxCollider>().enabled = true; // this is trigger collider

            if (stacked.Count <= maxSizeOfTargetGroup)
            {
                // remove the object from cinemachine target group
                cineCameraTargetGroup.RemoveMember(stackObj.transform);
            }

            //stackObj.GetComponent<BoxCollider>().size += new Vector3(0.5f, 0.5f, 0.5f);
            //stackObj.AddComponent<BoxCollider>();

            // remove player collectable
            Destroy(stackObj.GetComponent<Collectable>());

            // change to neutral collectable
            StartCoroutine(stackObj.GetComponent<NeutralCollectable>().SetMaterialToNeutral());

            // activate neutral material script      
            // stackObj.GetComponent<NeutralCollectable>().enabled = true;

            //stackObj.GetComponent<NeutralCollectable>().SetMaterialToNeutral();

            // remove it from list
            stacked.Remove(stackObj);

            // and destroy
            //Destroy(stackObj);
        }

        // clear the list, but delete all objects before that
        //stacked.Clear();
    }

    public void RemoveAllStacks()
    {
        for (int i = 0; i < stacked.Count; i++)
        {
            // if the stack is empty
            if (stacked.Count == 0) return;

            // local obj
            //GameObject moneyobj = stacked[stacked.Count - 1];

            if (stacked.Count <= maxSizeOfTargetGroup)
            {
                // remove the object from cinemachine target group
                cineCameraTargetGroup.RemoveMember(stacked[i].transform);
            }
            // remove it from list
            //stacked.Remove(moneyobj);

            // and destroy
            Destroy(stacked[i]); 
        }

        // clear the list, but delete all objects before that
        stacked.Clear();
    }

    //public void AddToStack(GameObject moneyObj)
    //{
    //    // add obj to list
    //    stacked.Add(moneyObj.gameObject);

    //    Rigidbody rb = moneyObj.gameObject.GetComponent<Rigidbody>();
    //    // remove rigidbody
    //    Destroy(rb);

    //    // disable colliders
    //    moneyObj.GetComponent<BoxCollider>().enabled = false;

    //    // set parent
    //    //moneyObj.gameObject.transform.SetParent(backpackObj.transform);

    //    // activate move component
    //    moneyObj.GetComponent<MoneyMovement>().SetMoveToVector(backpackObj.transform.position + new Vector3(0, stacked.Count * moneyObj.transform.localScale.y, 0));

    //    // set parent
    //    moneyObj.GetComponent<MoneyMovement>().SetParent(backpackObj.transform);

    //    // enable the script
    //    moneyObj.GetComponent<MoneyMovement>().enabled = true;


    //// ------------- AUDIO -----------------
    //    audioSource.clip = stackClip;
    //    audioSource.pitch = 0.8f + stacked.Count * 0.05f;
    //    audioSource.Play();
    //}

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

        // remove the object from cinemachine target group
        cineCameraTargetGroup.RemoveMember(moneyObj.transform);

        // ----------- AUDIO ----------------------
        PlayUnloadAudio();

        HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);

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

        // remove the object from cinemachine target group
        cineCameraTargetGroup.RemoveMember(moneyObj.transform);

        // set position
        moneyObj.transform.position = objPos;
        // scale up
        moneyObj.transform.localScale *= 1.5f;

        //set rotation
        moneyObj.transform.rotation = Quaternion.Euler(transform.localEulerAngles);

        // add box collider because enabling it will enable the trigger one
        BoxCollider collider = moneyObj.AddComponent<BoxCollider>();
        collider.size = new Vector3(2f, 0.7f, 1);
        // ----------- AUDIO ----------------------
        PlayUnloadAudio();

        HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);

    }

    private void PlayUnloadAudio()
    {
        audioSource.clip = payClip;
        audioSource.pitch = 0.5f + stacked.Count * 0.01f;
        audioSource.Play();
    }
    public int GetStackCount()
    {
        return stacked.Count;
    }

    //public void Apply(MultiplierDefinition applyMultiplier)
    //{
    //    throw new System.NotImplementedException();
    //}

    //public void RemoveMoney()
    //{
    //    // local obj
    //    GameObject moneyObj = stacked[stacked.Count - 1];

    //    // remove it from list
    //    stacked.Remove(moneyObj);
    //    // and destroy
    //    Destroy(moneyObj);

    //    audioSource.clip = payClip;
    //    audioSource.pitch = 0.5f + stacked.Count * 0.05f;
    //    audioSource.Play();
    //}
}

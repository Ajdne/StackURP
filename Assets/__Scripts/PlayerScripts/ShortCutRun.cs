using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortCutRun : MonoBehaviour
{
    [SerializeField] private GameObject raycastObj;
    private IStacking stacking;
    private IMovement movement;

    [SerializeField] private float endComboTime;
    [SerializeField] private GameObject speedTrailParticle;

    // FOR AI
    [SerializeField] private AIStateManager stateManager;

    private bool gotBoost;
    public bool GotBoost { get { return gotBoost; } }

    private float waitTimer;
    private int stackComboCounter;  // used to activate bonus movement speed

    void Start()
    {
        stacking = GetComponent<IStacking>();
        movement = GetComponent<IMovement>();
    }

    void Update()
    {
        waitTimer += Time.deltaTime;

        if (Physics.Raycast(raycastObj.transform.position, Vector3.down, 10))
        {
            
            //return;
        }
        // the AI knows it has no more stacks
        else if(this.gameObject.layer != 10 && stacking.GetStackCount() == 0)
        {
            // if the AI is out of stacks for shortcut run, switch to collect state to get more stacks
            stateManager.SwitchToCollectState();
        }
        else if (stacking.GetStackCount() > 0 && waitTimer > 0.02f)
        {
            stacking.RemoveStackToShortcut(new Vector3(raycastObj.transform.position.x, -0.25f, raycastObj.transform.position.z));

            stackComboCounter++;

            if (stackComboCounter == 10 && !gotBoost)
            {
                // increase movement speed from base to 50% higher
                movement.SetMovementSpeed(IMovement.baseMoveSpeed * 1.5f);

                speedTrailParticle.SetActive(true);

                gotBoost = true;
            }

            waitTimer = 0;
        }

        // if combo duration has passed and its not the end of a level, turn off boost
        if (waitTimer > endComboTime && !GameManager.Instance.IsEndGame)
        {
            DisableBoost();
        }
    }

    public void DisableBoost()
    {
        // reset move speed to original value
        movement.SetMovementSpeed(IMovement.baseMoveSpeed);

        speedTrailParticle.SetActive(false);

        // reset the bool
        gotBoost = false;

        // reset the counter
        stackComboCounter = 0;
    }
}

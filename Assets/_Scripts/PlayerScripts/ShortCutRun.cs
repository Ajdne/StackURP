using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortCutRun : MonoBehaviour
{
    [SerializeField] private GameObject raycastObj;
    private Stacking stacking;
    private Movement2 movement;

    [SerializeField] private float endComboTime;
    [SerializeField] private GameObject speedTrailParticle;
    private float originalMoveSpeed;    // save initial move speed value\
    private bool gotBonus;

    private float waitTimer;
    private int stackComboCounter;  // used to activate bonus movement speed

    void Start()
    {
        stacking = GetComponent<Stacking>();
        movement = GetComponent<Movement2>();

        originalMoveSpeed = movement.MoveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        waitTimer += Time.deltaTime;

        if (Physics.Raycast(raycastObj.transform.position, Vector3.down, 10))
        {
            
            //return;
        }
        else if (stacking.GetStackCount() > 0 && waitTimer > 0.02f)
        {
            stacking.RemoveStackToShortcut(new Vector3(raycastObj.transform.position.x, -0.25f, raycastObj.transform.position.z));

            stackComboCounter++;

            if (stackComboCounter == 10 && !gotBonus)
            {
                // increase movement speed
                movement.MoveSpeed *= 2f;

                speedTrailParticle.SetActive(true);

                gotBonus = true;
            }

            waitTimer = 0;
        }

        if (waitTimer > endComboTime && !GameManager.Instance.IsEndGame)
        {
            // reset move speed to original value
            movement.MoveSpeed = originalMoveSpeed;

            speedTrailParticle.SetActive(false);

            // reset the bool
            gotBonus = false;

            // reset the counter
            stackComboCounter = 0;
        }
    }
}

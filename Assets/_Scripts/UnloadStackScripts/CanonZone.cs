using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonZone : MonoBehaviour
{
    [Header("Connected Prefabs")]
    [SerializeField] private GameObject cannonTop;
    [SerializeField] private GameObject cannonBottom;
    [SerializeField] private GameObject cannonBarrelEnd;
    [SerializeField, Space(10)] private GameObject ammoPref;

    [Space(10)]
    [SerializeField] private GameObject shootParticle;
    [SerializeField, Space(10)] private int stacksNeeded;

    private List<GameObject> cannonAmmo = new List<GameObject>();
    private int ammoStacks;

    private float shootTimer;
    private float stayTimer;

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 3) // player layer
        {
            stayTimer += Time.deltaTime;

            shootTimer = 0;

            if (stayTimer > 0.1f && other.GetComponent<Stacking>().GetStackCount() > 0 && ammoStacks < stacksNeeded)
            {
                // remove money from the stack to teh cannon and dont destry the stack
                other.GetComponent<Stacking>().RemoveMoneyToProperty(cannonTop.transform.position, true);

                // spawn ammo in the tower
                GameObject ammo = Instantiate(ammoPref, cannonBottom.transform.position + new Vector3(0, cannonAmmo.Count * 0.2f, 0), Quaternion.Euler(-90, 0, 0));

                // add ammo stack to the ammo list
                cannonAmmo.Add(ammo);

                //increase tower top height
                UpdateCannonTopPosition();

                // increase ammo counter
                ammoStacks++;

                // reset the timer
                stayTimer = 0;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 3) // player layer
        {
            // reset the timer
            stayTimer = 0;
        }
    }

    void Update()
    {
        shootTimer += Time.deltaTime;

        if (shootTimer > 0.5f && cannonAmmo.Count > 0)
        {
            GameObject projectile = cannonAmmo[cannonAmmo.Count - 1];

            // set the projectile starting position to the barrel end
            projectile.transform.position = cannonBarrelEnd.transform.position;

            // set projectile destination
            projectile.GetComponent<MoneyMovement>().SetMoveToVector(new Vector3(cannonBarrelEnd.transform.position.x, 0, transform.position.z + stacksNeeded + cannonAmmo.Count), false);

            // decrease cannon top height
            UpdateCannonTopPosition();

            // activate projectile movement
            projectile.GetComponent<MoneyMovement>().enabled = true;

            //projectile.transform.rotation = Quaternion.Euler(-90, 0, 0);

            // remove that stack from ammo list
            cannonAmmo.Remove(projectile);

            // reset the timer
            shootTimer = 0;
        }
    }

    void UpdateCannonTopPosition()
    {
        cannonTop.transform.position = cannonBottom.transform.position + new Vector3(0, cannonAmmo.Count * 0.2f, 0);
    }
}

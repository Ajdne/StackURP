using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortCutRun : MonoBehaviour
{
    [SerializeField] private GameObject raycastObj;
    private Stacking stacking;

    private float waitTimer;

    void Start()
    {
        stacking = GetComponent<Stacking>();
    }

    // Update is called once per frame
    void Update()
    {
        waitTimer += Time.deltaTime;

        if (Physics.Raycast(raycastObj.transform.position, Vector3.down, 10)) return;
        else if (stacking.GetStackCount() > 0 && waitTimer > 0.08f)
        {
            stacking.RemoveStackToShortcut(new Vector3(raycastObj.transform.position.x, -0.25f, raycastObj.transform.position.z));

            waitTimer = 0;
        }
    }
}

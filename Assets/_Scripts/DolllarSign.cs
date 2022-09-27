using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DolllarSign : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 1f;
    [SerializeField] float floatSpeed;
    [SerializeField] float moveDistance;

    void Update()
    {
        Vector3 newPos = transform.position + new Vector3(0, Mathf.Sin(Time.timeSinceLevelLoad * floatSpeed) * moveDistance, 0);

        transform.position = newPos;

        transform.Rotate(new Vector3(0, rotateSpeed * Time.deltaTime, 0), Space.World);
    }
}

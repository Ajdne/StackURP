using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crown : MonoBehaviour
{
    [SerializeField] private int rotateSpeed;

    void Update()
    {
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0, Space.Self);    
    }
}

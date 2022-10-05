using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineTrigger : MonoBehaviour
{

    [SerializeField] private GameObject trigger;

  

    private void OnTriggerEnter(Collider other)
    {
        CinemachineState.instance.SwitchState();
    }
}

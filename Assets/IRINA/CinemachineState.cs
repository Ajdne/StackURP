using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineState : MonoBehaviour
{
    public static CinemachineState instance{ get; private set; }

    private Animator animator;
    private bool camera1 = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        if (instance == null)
            instance = this;
            
    }

    public void SwitchState()
    {
        if (camera1)
        {
            animator.Play("Camera1");

        }
        else
        {
            animator.Play("Camera2");
        }
        camera1 = !camera1;
    }
}

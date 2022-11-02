using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasLookAt : MonoBehaviour
{
    public static GameObject currentEmoji;

    void LateUpdate()
    {
        transform.LookAt(Camera.main.transform, Vector3.up);

        //float rotateAngle = Camera.main.transform.rotation.eulerAngles.y - transform.rotation.eulerAngles.y;
 
        //    //Vector3.forward - transform.rotation.y - Camera.main.transform.rotation.eulerAngles.y;
        ////transform.RotateAround(transform.position, Vector3.up, rotateAngle * Time.deltaTime);
        //transform.Rotate(0, rotateAngle * Time.deltaTime * 100, 0, Space.World);
    }

    public void DeactivateEmoji()
    {
        currentEmoji.SetActive(false);
    }

    public void ActivateEmoji()
    {
        currentEmoji.SetActive(true);
    }
}

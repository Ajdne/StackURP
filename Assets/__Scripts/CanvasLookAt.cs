using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasLookAt : MonoBehaviour
{
    private GameObject currentEmoji;
    public GameObject CurrentEmoji { get { return currentEmoji; } set { currentEmoji = value; } }

    void LateUpdate()
    {
        transform.LookAt(Camera.main.transform, Vector3.up);
    }

    public IEnumerator DeactivateEmoji()
    {
        yield return new WaitForSeconds(1.05f);

        currentEmoji.SetActive(false);
    }

    public void ActivateEmoji()
    {
        currentEmoji.SetActive(true);
    }
}

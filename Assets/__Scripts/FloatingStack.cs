using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingStack : MonoBehaviour
{
    private float timer;
    private int flySpeed = 2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(FlyAway());
        }
    }

    IEnumerator FlyAway()
    {
        yield return new WaitForSeconds(0.2f);

        while (timer < 12f)
        {
            // float up
            transform.Translate(0, flySpeed * Time.deltaTime, 0, Space.World);

            timer += Time.deltaTime;
            yield return null;
        }

        Destroy(this.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MultiplierPlatform : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int multiplierValue;
    [SerializeField] private TextMeshProUGUI multiplierValueCanvas;

    private bool isTriggered;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, 0, transform.position.z), speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3 && !isTriggered)
        {
            // POP! 
            // increase player speed
            GameManager.Instance.Player.GetComponent<Movement2>().MoveSpeed += 3f;

            // increase stacks
            //GameManager.Instance.Player.GetComponent<Stacking>().MultiplyStack(multiplierValue);

            isTriggered = true;
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    public float Speed { get { return speed; } set { speed = value; } }

    [SerializeField] private int rotateSpeed;

    [SerializeField] private BoxCollider collider; // the non-trigger collider

    private Vector3 moveToVector;
    private bool destroyStack;
    private Transform backpack;

    public void SetMoveToVector(Vector3 obj, bool destroy)
    {
        moveToVector = obj;
        destroyStack = destroy;
    }

    public void SetParent(Transform backpackObj)
    {
        backpack = backpackObj;
    }

    void Update()
    {
        
        transform.position = Vector3.MoveTowards(transform.position, moveToVector, speed * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(Vector3.forward), rotateSpeed * Time.deltaTime);

        if (transform.position == moveToVector)
        {
            if(destroyStack)
            {
                Destroy(this.gameObject);
                
            }
            else
            {
                this.gameObject.transform.localScale *= 2;
                // add colliders for bridge
                collider.enabled = true;
            }

            // disable this script
            this.enabled = false;
        }
    }
}

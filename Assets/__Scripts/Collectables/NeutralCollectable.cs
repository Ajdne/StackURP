using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NeutralCollectable : MonoBehaviour
{
    [Space(5f), Header("Material Settings"), Space(2f)]
    [SerializeField] private Material neutralMaterial;

    // apply adequate material once collected
    [SerializeField] private List<Material> playerMaterials;
    // must be placed in the right order - same as player layers
    // number 0 in list is blue, 1 is red etc

    [Space]
    [SerializeField] private BoxCollider brickObj;
    [SerializeField] private MeshRenderer mesh; // material component of this collectable
    [SerializeField] private bool isEnabled;

    [Space(5f)]
    [SerializeField] private Outline outline;

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Neutral aktivan");
        // here we only check if its a player
        if (other.CompareTag("Player")  && isEnabled)
        {
            // check the layer of the player who collected the brick and use his color
            mesh.material = playerMaterials[other.gameObject.layer - 10];

            other.GetComponent<IStacking>().AddMoneyToStack(this.gameObject);

            outline.enabled = false;

            // return the layer to default
            // need this in order for shortcut run to work properly
            //this.gameObject.layer = 0;
        }

        //if (other.gameObject.layer == 6) // platform layer
        //{
        //    this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        //}
    }

    public IEnumerator SetMaterialToNeutral()
    {
        mesh.material = neutralMaterial;

        // enable box collider
        brickObj.enabled = true;

        outline.enabled = true;

        yield return new WaitForSeconds(1);
        isEnabled = true;


    }
}

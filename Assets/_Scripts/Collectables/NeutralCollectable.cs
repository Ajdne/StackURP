using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutralCollectable : MonoBehaviour
{
    [Space(5f), Header("Material Settings"), Space(2f)]
    [SerializeField] private Material neutralMaterial;

    // apply adequate material once collected
    [SerializeField] private List<Material> playerMaterials;
    // must be placed in the right order - same as player layers
    // number 0 in list is blue, 1 is red etc

    [SerializeField] private MeshRenderer mesh; // material component of this collectable

    private void OnEnable()
    {
        mesh.material = neutralMaterial;
    }

    void OnTriggerEnter(Collider other)
    {
        // here we only check if its a player
        if (other.CompareTag("Player"))
        {
            other.GetComponent<IStacking>().AddMoneyToStack(this.gameObject);

            // check the layer of the player who collected the brick and use his color
            mesh.material = playerMaterials[other.gameObject.layer - 10];

            // return the layer to default
            // need this in order for shortcut run to work properly
            this.gameObject.layer = 0; 
        }
    }

    public void SetMaterialToNeutral()
    {
        mesh.material = neutralMaterial;
    }
}

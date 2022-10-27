using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Material Holder", menuName = "ScriptableObjects/Material Holder")]
public class MaterialHolderSO : ScriptableObject
{
    [SerializeField] private List<Material> playerMaterials;
    public List<Material> PlayerMaterials { get { return playerMaterials; } }

}

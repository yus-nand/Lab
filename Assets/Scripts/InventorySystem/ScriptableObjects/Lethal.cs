using UnityEngine;
using UnityEngine.Rendering;
[CreateAssetMenu(fileName = "Lethal", menuName = "ScriptableObjects/Item/Lethal")]
public class Lethal : ItemObject
{
    void Awake()
    {
        itemtype = Type.Lethal;
    }
    public override void Use()
    {
        Debug.Log("Using " + itemName);
    }
}

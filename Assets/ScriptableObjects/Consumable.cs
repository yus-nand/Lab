using UnityEngine;
[CreateAssetMenu(fileName = "Consumable", menuName = "ScriptableObjects/Item/Consumable")]

public class Consumable : ItemObject
{
   void Awake()
    {
        itemtype = Type.Consumable;
    }
    public override void Use()
    {
        Debug.Log("Using " + itemName);
    }
}

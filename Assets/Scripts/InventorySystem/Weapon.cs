using UnityEngine;
[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Item/Weapon")]

public class Weapon : Item
{
    void Awake()
    {
        itemtype = Type.Weapon;
    }
    public override void Use()
    {
        Debug.Log("Using " + itemName);
    }
}
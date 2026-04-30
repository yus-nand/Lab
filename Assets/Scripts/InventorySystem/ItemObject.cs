using UnityEngine;
public enum Type
{
    Weapon,
    Consumable,
    Lethal
}
[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObjects/Item")]
public abstract class ItemObject : ScriptableObject
{
    public string itemID;
    public string itemName;
    public Sprite itemIcon;
    public Type itemtype;
    public bool isStackable;
    public int maxStackSize = 1;
    public abstract void Use(); // will remove this later
}

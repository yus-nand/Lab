using UnityEngine;
public enum Type
{
    Weapon,
    Consumable,
    Lethal
}
public abstract class Item : ScriptableObject
{
    public int itemID;
    public string itemName;
    public Sprite itemIcon;
    public Type itemtype;
    public abstract void Use();
}

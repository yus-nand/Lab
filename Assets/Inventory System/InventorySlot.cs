[System.Serializable]
public class InventorySlot
{
    public ItemObject item;
    public int quantity;

    public InventorySlot(ItemObject item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;
    }
    public bool IsEmpty
    {
        get
        {
            return item == null;
        }
    }
    public void Clear()
    {
        item = null;
    }
}

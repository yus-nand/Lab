[System.Serializable]
public class InventorySlot
{
    public ItemInstance item;
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

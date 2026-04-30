[System.Serializable]
public class ItemInstance
{
    public ItemObject itemObject;
    public int quantity;
    public ItemInstance(ItemObject itemObject, int quantity)
    {
        if(itemObject != null)
        {
            this.itemObject = itemObject;
            this.quantity = quantity;   
        }
    }
    // void OnTriggerEnter(Collider other)
    // {
    //     if(other.CompareTag("Player"))
    //     {
    //         Debug.Log("Picked up "+itemObject.itemName);
    //         itemObject.Use();
    //         Destroy(gameObject);
    //     }
    // }
}

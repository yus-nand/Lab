using UnityEngine;

public class WorldItem : MonoBehaviour  // add this script to any item prefab that can be picked up by player
{
    public ItemObject itemObject;
    public int quantity = 1;
    
    private Vector3 startPosition;
    public float bobSpeed = 2f;
    public float bobAmount = 0.2f;

    private void Awake()
    {
        gameObject.tag = "WorldItem";
    }
    private void Start()
    {
        startPosition = transform.position;
    }

    public void Pickup(Inventory inventory)
    {
        if(inventory == null)
        {
            Debug.LogError("NO INVENTORY PASSED");
            return;
        }
        Debug.Log($"storing {name}");
        bool success = inventory.AddItem(itemObject, quantity);
        if(success)
        {
            Debug.Log($"stored {name}");
            Destroy(gameObject);
        }
        else
        {
            Debug.Log($"WorldItem: {name} cant be stored");
        }
    }
    void OnTriggerEnter(Collider other)             // for testing purposes
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log($"Picking up {name}");
            Pickup(other.GetComponentInParent<Player>().inventory);
        }
    }
    void Update()
    {
        RotateAndBob();
    }
    void RotateAndBob()
    {
        transform.Rotate(Vector3.up, 1.2f);
        Vector3 newPosition = startPosition;
        newPosition.y += Mathf.Sin(Time.time * bobSpeed) * bobAmount;
        transform.position = newPosition;
    }
}    

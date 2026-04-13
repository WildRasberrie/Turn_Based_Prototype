using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : MonoBehaviour
{
   DistanceDetection distanceDetection;
   bool pickup;
   int potion = 0;
   public InventoryManager inventoryManager;
   public GameObject inventory;

    private void Start()
    {
        distanceDetection = GetComponent<DistanceDetection>();
        inventory = GameObject.Find("Inventory");
        inventory.SetActive(false);
    }
    private void Update()
    {
        if (interacted )
        {
            var anim = GetComponent<Animator>();
            //play open anim 
            anim.Play("Open Chest");
            //add item to inventory
                pickup = true;
            AddItemToInventory();
        }

        InventorySystem();
    }
    public void InventorySystem()
    {
        if (inventory != null)
        {
            if (requested_inventory)
            {
                print("Inventory Opened");
                print("Potions: " + potion);
                //display inventory UI
                //find inventory in scene and update it with potion count
                inventory.SetActive(true);

            }
            //if esc is pressed close inventory
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                inventory.SetActive(false);
            }
        }
    }
    public void AddItemToInventory()
    {
        if (pickup)
        {
            //play pickup animation
            //StartCoroutine(AddInventory());
            potion++;
            pickup = false;
        }
    }

    IEnumerator AddInventory()
    {
        yield return new WaitForSeconds(1f);
        var anim = GetComponent<Animator>();
        anim.Play("Item Pickup");
    }
    public bool interacted => distanceDetection && Input.GetKeyDown(KeyCode.E);

    public bool requested_inventory => Input.GetKey(KeyCode.I);
}
using UnityEngine;

public class OpenInventory : MonoBehaviour
{
    public InventoryManager inventoryManager;

    // Update is called once per frame
    void Update()
    {
        if (requested_inventory) inventoryManager.EnterInventorySystem();
        if (requested_exit_inventory) inventoryManager.ExitInventorySystem();

    }

    public bool requested_inventory => Input.GetKeyDown(KeyCode.I);

    public bool requested_exit_inventory => Input.GetKeyDown(KeyCode.X);
}


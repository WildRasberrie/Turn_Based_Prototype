using UnityEngine;
using UnityEngine.UI;

public class SetOnClickEvent : MonoBehaviour
{
    public Button inventoryButton;
    public SceneLoader SceneLoader;
    public bool clicked;
    public GameObject Inventory;
    void Start()
    {
        inventoryButton = GetComponentInChildren<Button>();

        SceneLoader = GameObject.FindWithTag("SceneLoader").GetComponent<SceneLoader>();

    }

    public void OnClick() => clicked = true;
        
    public bool requested_inventory => Input.GetKeyUp(KeyCode.I);
    
    public bool close_inventory => Input.GetKeyUp(KeyCode.X);


    void Update()
    {
        if (clicked || requested_inventory)
        {
            Inventory.SetActive(true);
        }
        else if (Inventory.activeSelf == true) 
        {
            clicked = false;
            if (close_inventory || clicked)
            {

                Inventory.SetActive(false);

            }
        }

    }


}

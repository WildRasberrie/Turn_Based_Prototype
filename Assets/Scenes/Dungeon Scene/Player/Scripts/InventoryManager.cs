using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class InventoryManager : MonoBehaviour
{
    ItemPickup ItemPickup;
    public GameObject[] interactables;
    [Header("Inventory Selection Script")]
    public InventorySelect InventorySelect;
    [Space]

    [Header("Inventory Stats")]
    public int potion = 0;
    public GameObject inventory;
    public bool clicked;
    public bool requested_inventory;
    public Animator bag;

    void Start()
    {
        GetTag();

    }

    void Update()
    {
        InventorySystem();
    }
    void GetTag()
    {
        if (inventory == null && SceneManager.GetActiveScene().name != "IntroScene")
        {
            inventory = GameObject.Find("Inventory");

        }
        if (inventory != null)
        {
            //find Inventory Select Script
            if (InventorySelect == null) InventorySelect = GameObject.Find("Items").GetComponent<InventorySelect>();
            if (bag == null)
            {
                bag = GameObject.Find("Inventory Bag").GetComponentInChildren<Animator>();
                clicked = GameObject.Find("Inventory Bag").GetComponent<SetOnClickEvent>().clicked;
                requested_inventory = GameObject.Find("Inventory Bag").GetComponent<SetOnClickEvent>().requested_inventory;
          
            }

            inventory.SetActive(false);
        }
        interactables = GameObject.FindGameObjectsWithTag("Interactable");
        //get item pickup script 
        for (int i = 0; i < interactables.Length; i++)
        {
            ItemPickup = interactables[i].GetComponent<ItemPickup>();
        }
    }


    public void InventorySystem()
    {
        if (SceneManager.GetActiveScene().name != "IntroScene")
        {
            if (inventory != null)
            {

                if (requested_inventory || clicked)
                {
                    //play anim 
                    StartCoroutine(OpenInventory());
                    //play sound 
                    StartCoroutine(PlayUI());

                    print("Inventory Opened");

                }
                //if X is pressed close inventory
                if (inventory.activeSelf == true)
                {
                    if (Input.GetKeyDown(KeyCode.X) || clicked)
                    {
                        //play sound 
                        StartCoroutine(PlayUI());
                        inventory.SetActive(false);
                        //clicked = false 
                        clicked = false;
                        //set click to link with on Click Event
                        GameObject.Find("Inventory Bag").GetComponent<SetOnClickEvent>().clicked = clicked;

                    }
                }

                //if potion used add to player MP 
                if (InventorySelect.addMP)
                {
                    //refill player MP 
                    StartCoroutine(IncreaseMP());
                    InventorySelect.addMP = false;

                }
            }
        }
    }

    IEnumerator OpenInventory()
    {
        yield return new WaitForSeconds(1f);
        bag.Play("Open");
    }
    public IEnumerator PlayUI()
    {
        AudioLibrary.Instance.PlaySound(Sfx.Clicked_UI);
        yield return new WaitForSeconds(1f);

    }

    IEnumerator IncreaseMP()
    {
        AudioLibrary.Instance.PlaySound(Sfx.Increase_Stats);
        yield return new WaitForSeconds(1f);
        var MP_Refill = 50;
        for (int i = 0; i < MP_Refill; i++)
        {
            yield return new WaitForSeconds(1f);
            i++;
            SceneLoader SceneLoader = GameObject.FindWithTag("SceneLoader").GetComponent<SceneLoader>();
            SceneLoader.playerMP += i;
        }

    }
}

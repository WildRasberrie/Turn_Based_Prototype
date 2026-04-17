using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    ItemPickup ItemPickup;
    public GameObject[] interactables;
    [Header("Inventory Selection Script")]
    public InventorySelect InventorySelect; 

    [Header ("Player Stats")]
    public int playerHP = 100; // start player at 100 hp 
    public int playerMP = 50;

    [Space]

    [Header ("Inventory Stats")]
    public int potion = 0;
    public GameObject inventory;
    public bool clicked;
    public Animator bag; 

    void Awake()
    {
        DontDestroyOnLoad(this);
        GetTag();

        //grab Inventory component and hide it

        if (inventory == null) { 
            inventory = GameObject.Find("Inventory");
        }
        if (inventory != null)
        {
            inventory.SetActive(false);
        }
    }

    void Update()
    {
        InventorySystem();
    }

    public void LoadScene(string sceneName)
    {
            SceneManager.LoadScene(sceneName);
    }


    //any file with the interactable tag get Item pickup script
    void GetTag() {
        interactables = GameObject.FindGameObjectsWithTag("Interactable");
        //get item pickup script 
        for (int i = 0; i < interactables.Length; i++) {
            ItemPickup = interactables[i].GetComponent<ItemPickup>();
        }
    }

    public bool requested_inventory => Input.GetKeyUp(KeyCode.I);

    public void Clicked()
    {
        clicked = true;
    }
    public void InventorySystem()
    {
        if (inventory != null)
        {
            if (requested_inventory || clicked)
            {
                //play anim 
                //StartCoroutine(OpenInventory());
                //play sound 
                StartCoroutine(PlayUI());
                
                print("Inventory Opened");
                //display inventory UI
                //find inventory in scene and update it with potion count
                inventory.SetActive(true);
                //if Inventory opened 
                //find Inventory Select Script
                GameObject.Find("Items").GetComponent<InventorySelect>();
            }
            //if esc is pressed close inventory
            if (inventory.activeSelf == true)
            {
                if (Input.GetKeyDown(KeyCode.X) || clicked)
                {
                    //play sound 
                    StartCoroutine (PlayUI());
                    inventory.SetActive(false);
                    clicked = false;
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

    IEnumerator OpenInventory() {
        yield return new WaitForSeconds(1f);
        bag.Play("Open");
    }
    public IEnumerator PlayUI() { 
        AudioLibrary.Instance.PlaySound(Sfx.Clicked_UI);
        yield return new WaitForSeconds(1f);

    }

    IEnumerator IncreaseMP() {
        AudioLibrary.Instance.PlaySound(Sfx.Increase_Stats);
        yield return new WaitForSeconds(1f);
        var MP_Refill = 50;
        for (int i = 0; i < MP_Refill; i++) {
            yield return new WaitForSeconds(1f);
            i++;
            playerMP += i;
        }
        
    }
}


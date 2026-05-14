using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class InventoryManager : MonoBehaviour
{
    [Header("Chests")]
    public GameObject[] interactables;
    [Header ("Potions Game Object")]
    public GameObject potionGO;
    [Header("Inventory Selection Script")]
    public InventorySelect InventorySelect;
    [Space]

    [Header("Inventory Stats")]
    public int potion = 0;
    public GameObject inventory;
    public Animator bag;

    void Start()
    {
        GetTag();
        inventory.SetActive(false);

    }

    void Update()
    {
        if (potion == 0) { potionGO.SetActive(false); }
        else { potionGO.SetActive(true); }

    }
    void GetTag()
    {
        if (inventory == null && SceneManager.GetActiveScene().name != "IntroScene")
        {
            inventory = GameObject.Find("Inventory");
            //find Inventory Select Script
            if (InventorySelect == null) InventorySelect = GameObject.Find("Items").GetComponent<InventorySelect>();
            if (bag == null)
            {
                bag = GameObject.Find("Inventory Bag").GetComponentInChildren<Animator>();

            }

            inventory.SetActive(false);
        }
        interactables = GameObject.FindGameObjectsWithTag("Interactable");    

    }


    public void EnterInventorySystem() {
        inventory.SetActive(true);
        //play anim 
        StartCoroutine(OpenInventory());
        //play sound 
        StartCoroutine(PlayUI());

        print("Inventory Opened");

    }
    public void ExitInventorySystem() {
        if (inventory.activeSelf == false) return;
        //play sound 
        StartCoroutine(PlayUI());
        inventory.SetActive(false);
    }

    public void AddMP() { 
                //if potion used add to player MP 
                if (InventorySelect.addMP)
                {
                    //refill player MP 
                    StartCoroutine(IncreaseMP());
                    InventorySelect.addMP = false;

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

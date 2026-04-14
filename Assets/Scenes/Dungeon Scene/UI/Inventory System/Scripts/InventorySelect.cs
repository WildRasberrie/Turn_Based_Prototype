using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InventorySelect : MonoBehaviour
{
    [Header("Interactable Potion Button")]
    public Button[] Button;
    [Header("Potion Inventory")]
    public GameObject[] Potions;
    int index = 0;
    bool potion_selected;
    ItemPickup itemPickup;
    public TextMeshProUGUI item_inventory;
    public TextMeshProUGUI UI_Prompt;
    public GameObject UI_Sprite;
    SceneLoader SceneLoader;
    public GameObject inventory;

    bool yes, no;
    void Awake()
    {
        UI_Sprite.SetActive(false);
        //Grab item pickup script from parent
        if (itemPickup == null)
        {
            itemPickup = GameObject.Find("SceneLoader").GetComponent<ItemPickup>();
        }

        if (Potions != null)
        {
            var Potions_Length = GameObject.FindGameObjectsWithTag("Potion").Length;
            //Grab all potions in the inventory
            for (int i = 0; i < Potions_Length; i++)
            {
                Potions[i] = GameObject.FindGameObjectsWithTag("Potion")[i];

                //grab select button on each potion
                Button[i] = Potions[i].GetComponentInChildren<Button>();

                //add on click event to each button
                // assign game object for on click event to select potion
                Button[i].onClick.AddListener(SelectPotion);
            }
        }
        //grab Scene Loader 
        SceneLoader = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();
        //grab Inventory component and hide it
        inventory = GameObject.Find("Inventory");
        if (inventory != null)
        {
            inventory.SetActive(false);
        }
    }
    void Update()
    {
        //if no potions available, make potion sprite inactive 
        if (SceneLoader.potion <= 0)
        {
            print("You have" + SceneLoader.potion + "potion(s) available!");
            for (int i = 0; i < Potions.Length; i++)
            {
                Potions[i].SetActive(false);
            }
        }
        
        else
        {
            for (int i = 0; i < Potions.Length; i++)
            {
                Potions[i].SetActive(true);
            }
        }

        if (index >= Potions.Length) index = Potions.Length;

         // show amount of potions available
        item_inventory.text = "" + SceneLoader.potion;

        if (potion_selected)
        {
            //set UI to active 
            UI_Sprite.SetActive(true);
            // open UI prompt
            UI_Prompt.text = "Are you sure you want to use " + Potions[0].name + "?";

        }
        //if yes is clicked, start drink anim 
        if (yes)
        {
            potion_selected = false;
            UI_Sprite.SetActive(false);

            StartCoroutine(DrinkUp());
            yes = false;
        }
        else if (no) {
            potion_selected = false;
            UI_Sprite.SetActive(false);
        }

    }

    public void YesButton() {
        yes = true;

    }

    public void NoButton() {
        no = true;
    }
    public void SelectPotion()
    {
        potion_selected = true;
    }

    IEnumerator DrinkUp()
    {
        yield return new WaitForSeconds(0.5f);

        Animator potion_anim;
        potion_anim = Potions[Potions.Length-1].GetComponentInChildren<Animator>();
        potion_anim.Play("Drink Up");

        if (SceneLoader.potion > 0)  SceneLoader.potion -= 1;

    }

    public bool requested_inventory => Input.GetKey(KeyCode.I);
    public void InventorySystem()
    {
        if (inventory != null)
        {
            if (requested_inventory)
            {
                print("Inventory Opened");
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
}

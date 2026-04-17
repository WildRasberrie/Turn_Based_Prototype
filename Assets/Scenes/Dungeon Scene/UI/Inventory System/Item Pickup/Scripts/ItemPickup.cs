using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : MonoBehaviour
{
   DistanceDetection distanceDetection;
   public bool pickup;
   public GameObject inventory;
    SceneLoader SceneLoader;
    public bool interacted;
    Animator anim;
    public GameObject interact_text;

    private void Awake()
    {
        SceneLoader = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        distanceDetection = GetComponent<DistanceDetection>();
        
    }
    void Update()
    {
        interacted = distanceDetection && Input.GetKeyDown(KeyCode.E);


        if (distanceDetection)
        {
            interact_text.SetActive(true);
        }
        else {
            interact_text.SetActive(false);
        }

        if (interacted)
        {
            //play open anim 
            StartCoroutine(PlayChestOpenSound());
            // Start add to inventory
            //add item to inventory
            pickup = true;
            StartCoroutine(AddInventory());
        }

    }

    IEnumerator PlayChestOpenSound() {

        AudioLibrary.Instance.PlaySound(Sfx.Open_Chest);
        yield return new WaitForSeconds(1f);
        anim.Play("Open Chest");

    }

    IEnumerator AddInventory()
    {
        yield return new WaitForSeconds(2f);
        anim.Play("Item Pickup");
        yield return new WaitForSeconds(1f);

        AudioLibrary.Instance.PlaySound(Sfx.Gained_Item);


        SceneLoader.potion++;
        yield return new WaitForSeconds(1f);
        pickup = false;
    }

}
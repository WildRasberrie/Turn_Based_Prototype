using System.Collections;
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

        if (interacted )
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
        yield return new WaitForSeconds(1f);
        AudioLibrary.Instance.PlaySound(Sfx.Gained_Item);
        yield return new WaitForSeconds(1f);
        anim.Play("Item Pickup");

        SceneLoader.potion++;
        yield return new WaitForSeconds(1f);
        pickup = false;
    }

}
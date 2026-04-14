using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : MonoBehaviour
{
   DistanceDetection distanceDetection;
   public bool pickup;
   public GameObject inventory;
    SceneLoader SceneLoader;
    private void Awake()
    {
        SceneLoader = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();
    }
    void Start()
    {
        distanceDetection = GetComponent<DistanceDetection>();
        
    }
    void Update()
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

    }
    
    public void AddItemToInventory()
    {
        if (pickup)
        {
            //play pickup animation
            StartCoroutine(AddInventory());
            SceneLoader.potion++;
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

}
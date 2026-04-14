using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    ItemPickup ItemPickup;
    public GameObject[] interactables;
    public int potion = 0;
    void Awake()
    {
        DontDestroyOnLoad(this);
        GetTag();
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


}


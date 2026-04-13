using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this);
        GetTag();
    }

    public void LoadScene(string sceneName)
    {
            SceneManager.LoadScene(sceneName);
    }
    //any file with the interactable tag give itempickup script
    void GetTag() { 
        GameObject[] interactables = GameObject.FindGameObjectsWithTag("Interactable");
        for (int i = 0; i < interactables.Length; i++)
        {
            interactables[i].AddComponent<ItemPickup>();
        }
    }

}


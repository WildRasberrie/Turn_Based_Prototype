using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [Header ("Player Stats")]
    public int playerHP = 100; // start player at 100 hp 
    public int playerMP = 50;
    [Header ("Amount of Battles Won")]
    public int battlesWon = 0; // start player at 0 battles won

    public Vector3 storedPosition; // store player position for scene transitions

    void Update()
    {
       
        ResetGame();
    }

    public void LoadScene(string sceneName)
    {
            SceneManager.LoadScene(sceneName);
    }


  

    void ResetGame()
    {
        if (SceneManager.GetActiveScene().name != "IntroScene")
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("IntroScene");
            }
        }
        if (SceneManager.GetActiveScene().name == "IntroScene") {
            //reset player stats 
            playerMP = 50;
            playerHP = 100;
        }
    }
}


using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [Header ("Player Stats")]
    public int playerHP = 100; // start player at 100 hp 
    public int playerMP = 50;


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


using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroButtonAction : MonoBehaviour
{

    public void OnStart() {
        SceneManager.LoadScene("Dungeon_lvl1");
        
    }

    public void OnQuit()
    {
        //Debug Log notifying of quitting game 
        Debug.Log("Application Quitting");
        //Quit in editor 
        UnityEditor.EditorApplication.isPlaying = false; 
        //Quit Game
        Application.Quit();
    }

}

using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipScene : MonoBehaviour
{
    public GameObject text;
    public void ShowHoverText()
    {
        // Show hover text when the player hovers over the skip button
        text.SetActive(true);
    }

    public void HideHoverText()
    {
        // Hide hover text when the player stops hovering over the skip button
        text.SetActive(false);
    }

    public void SkipToDungeon()
    {
       SceneManager.LoadScene("Dungeon_lvl1");
    }

    public void SkipToCutScene()
    {
        SceneManager.LoadScene("CutScene");
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class IntroButtonAction : MonoBehaviour
{
    public Animator anim;
    public GameObject[] SetInactiveOnZoom;
    [Header ("Interactable Buttons")]
    public EventTrigger[] buttons;
   

    public void OnStart() {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].enabled = false;
        }
        StartCoroutine(ZoomIn());

        for (int i = 0; i < SetInactiveOnZoom.Length; i++)
        {
            SetInactiveOnZoom[i].SetActive(false);
        }

     

    }

    public void OnQuit()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].enabled = false;
        }

        //Quit Game
        Application.Quit();
    }

    IEnumerator ZoomIn() { 
        yield return new WaitForSeconds(.1f);
        anim.Play("ZoomIn");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("DialogueScene");



    }
}

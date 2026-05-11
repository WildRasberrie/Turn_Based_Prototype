using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public class IntroButtonAction : MonoBehaviour
{
    public Animator anim;
    public GameObject[] clouds;

    void Start()
    {
        clouds = GameObject.FindGameObjectsWithTag("Clouds");
    }
    void Update()
    {
        for (int i = 0; i < clouds.Length; i++)
        {
            clouds[i] = GameObject.FindGameObjectWithTag("Clouds");
        }

    }

    public void OnStart() {
        StartCoroutine(ZoomIn());

        for (int i = 0; i < clouds.Length; i++)
        {
            clouds[i].SetActive(false);
        }

     

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

    IEnumerator ZoomIn() { 
        yield return new WaitForSeconds(.1f);
        anim.Play("ZoomIn");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("DialogueScene");



    }
}

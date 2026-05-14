using System.Collections;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioLibrary AudioLibrary;
    public RectTransform lightning;
    void Start()
    {
        AudioLibrary.PlaySound(Sfx.Howl);
        StartCoroutine(PlayMusic());
    }

    // Update is called once per frame
    void Update()
    {
        


    }

    public IEnumerator PlayLightning()
    {
        AudioLibrary.PlaySound(Sfx.Lightning);
        yield return new WaitForSeconds(0.1f);
    }

    public IEnumerator PlayMusic() {
        yield return new WaitForSeconds(0.5f);

        AudioLibrary.PlaySound(Sfx.Tone);
    }
}

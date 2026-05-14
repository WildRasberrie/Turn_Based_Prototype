using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour
{
    public AudioLibrary AudioLibrary;
    public IEnumerator PhoneRinging(){
         AudioLibrary.PlaySound(Sfx.Ringing);
         yield return new WaitForSeconds(3f);
         AudioLibrary.PlaySound(Sfx.Pickup);
         yield return new WaitForSeconds(0.5f);
    }

    public IEnumerator PhoneHangup(){
         AudioLibrary.PlaySound(Sfx.Pickup);
         yield return new WaitForSeconds(0.5f);
    }
    public IEnumerator PlayerSpeak()
    {
        AudioLibrary.PlaySound(Sfx.Player_Speak);
        yield return new WaitForSeconds(0.5f);
    }

    public void PlayPlayerSpeak() => StartCoroutine(PlayerSpeak());
   

    public IEnumerator AnonSpeak()
    {
        AudioLibrary.PlaySound(Sfx.Static);
        yield return new WaitForSeconds(0.5f);

        AudioLibrary.PlaySound(Sfx.Anon_Speak);
        yield return new WaitForSeconds(0.5f);
    }

}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioLibrary : MonoBehaviour
{
    public static AudioLibrary Instance;
    public List<SoundType> Sounds;
    public AudioSource Audio;
    public Dictionary<Sfx, AudioClip> SoundDict = new Dictionary<Sfx, AudioClip>();

    void Awake()
    {
        Instance = this;
        foreach (SoundType s in Sounds)
        {
            SoundDict.Add(s.Type, s.Clip);
        }
    }

    public AudioClip GetAudio(Sfx a)
    {
        if (SoundDict.ContainsKey(a)) return SoundDict[a];
        return null;
    }

    public void PlaySound(Sfx a)
    {
        Audio.PlayOneShot(GetAudio(a));
    }

}

[System.Serializable]
public class SoundType
{
    public Sfx Type;
    public AudioClip Clip;
}

public enum Sfx
{
    None = 0,
    Open_Chest = 1,
    Gained_Item = 2,
    Clicked_UI = 3,
    Increase_Stats = 4,
    Attack = 5,
    Taunt = 6,
    Tone = 7, 
    Hurt = 8,
    Dead = 9,
    Magic_attack = 10,
    Slap = 11
}
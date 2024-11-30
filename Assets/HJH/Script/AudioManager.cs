using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SerializedMonoBehaviour
{

    public AudioSource bgm;
    [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.Foldout)]
    public Dictionary<SFX,AudioClip> audioDic= new Dictionary<SFX,AudioClip>();

    [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.Foldout)]
    public Dictionary<SFX,AudioSource> audioCheck = new Dictionary<SFX,AudioSource>();


    public void StartAudioLoop(SFX sfx)
    {
        
        if (audioCheck.ContainsKey(sfx))
        {
            audioCheck[sfx].Play();
        }
        else
        {
            GameObject au = new GameObject(sfx.ToString());
            au.transform.parent = transform;
            AudioSource auu =  au.AddComponent<AudioSource>();
            auu.clip = audioDic[sfx];
            auu.loop = true;
            auu.Play();
            audioCheck[sfx] = auu;
        }
    }

    public void StartAudio(SFX sfx)
    {
        if (audioCheck.ContainsKey(sfx))
        {
            audioCheck[sfx].Play();
        }
        else
        {
            GameObject au = new GameObject(sfx.ToString());
            au.transform.parent = transform;
            AudioSource auu = au.AddComponent<AudioSource>();
            auu.clip = audioDic[sfx];
            auu.Play();
            audioCheck[sfx] = auu;
        }
    }

    public void StopAudio(SFX sfx)
    {
        if (audioDic.ContainsKey(sfx))
        {
            audioCheck[sfx].Stop();
        }
    }

    public void StopBGM()
    {
        bgm.Stop();
    }

    public void StartBGM()
    {
        bgm.Play();
    }
}

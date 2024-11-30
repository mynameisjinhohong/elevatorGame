using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class AudioManager : SerializedMonoBehaviour
{

    public AudioSource bgm;
    [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.Foldout)]
    public Dictionary<SFX,AudioClip> audioDic= new Dictionary<SFX,AudioClip>();

    [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.Foldout)]
    public Dictionary<SFX,AudioSource> audioCheck = new Dictionary<SFX,AudioSource>();


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
            AudioSource auu =  au.AddComponent<AudioSource>();
            auu.clip = audioDic[sfx];
            auu.loop = true;
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
}

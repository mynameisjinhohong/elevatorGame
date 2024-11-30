using System;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    public AudioClip buttonSFX;
    public AudioClip angrySFX;
    public AudioClip footstepSFX;
    public AudioClip evMoveSFX;
    public AudioClip doorOpenSFX;
    public AudioClip doorCloseSFX;
    public AudioSource SFXSource;
    public static SoundManager instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    


    public void PlayButtonSFX()
    {
        SFXSource.PlayOneShot(buttonSFX);
    }

    public void PlayAngrySFX(){
        SFXSource.PlayOneShot(angrySFX);
    }

    public void PlayEvSFX(){
        //
    }

    public void PlayFootstepSFX(){
        SFXSource.PlayOneShot(footstepSFX);
    }
}

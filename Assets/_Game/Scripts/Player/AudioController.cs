using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    //* variaveis atribuidas no inspector
    [SerializeField]private AudioSource audioSourceSfx;
    [SerializeField]private AudioClip shoot;
    [SerializeField]private AudioClip takeHit;
    [SerializeField]private AudioClip dead;
    

    private void Start()
    {
        audioSourceSfx = GetComponent<AudioSource>();
    }
    public void TocarSfx(AudioClip clip){

        audioSourceSfx.PlayOneShot(clip);
    }

    public void PlayShoot(){
        audioSourceSfx.PlayOneShot(shoot);
    }
    public void PlayTakeHit(){
        audioSourceSfx.PlayOneShot(takeHit);
    }
    public void PlayDead(){
        audioSourceSfx.PlayOneShot(dead);
    }
}

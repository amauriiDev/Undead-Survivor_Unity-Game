using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    // atributos anexados no inspector
    [SerializeField]AudioSource audioSourceSfx;
    [SerializeField]AudioClip selectSfx;
    [SerializeField]AudioClip levelUpSfx;
    [SerializeField]AudioClip loseSfx;
    [SerializeField]AudioClip winSfx;

    public void PlaySfx(AudioClip clip){
        audioSourceSfx.PlayOneShot(clip);
    }
    public void PlaySelectSfx(){
        audioSourceSfx.PlayOneShot(selectSfx);
    }
    public void PlayLevelUpSfx(){
         audioSourceSfx.PlayOneShot(levelUpSfx);
    }
    public void PlayLoseSfx(){
         audioSourceSfx.PlayOneShot(loseSfx);
    }
    public void PlayWinSfx(){
         audioSourceSfx.PlayOneShot(winSfx);
    }
}

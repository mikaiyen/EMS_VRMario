using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AudioManager : MonoBehaviour
{
    [Header("---------Audio Source---------")]
    [SerializeField] AudioSource bgm;
    [SerializeField] AudioSource sfx;
    [Header("---------Audio Clip SF---------")]
    public AudioClip collectitem;
    public AudioClip hitmysterybox;
    public AudioClip shootbullet;
    public AudioClip gethit;
    public AudioClip enemydeath;
    public AudioClip jump;
    public AudioClip teleportSound;

    [Header("---------Audio Clip BGM---------")]
    public AudioClip mainbgm;
    public AudioClip bossbgm;
    public AudioClip winbgm;
    public AudioClip losebgm;
    public AudioClip starbgm;

    private void Start(){
        bgm.clip=mainbgm;
        bgm.Play();
    }

    public void playSFX(AudioClip clip){
        sfx.PlayOneShot(clip);
    }

    public void switchbgm(AudioClip music){
        bgm.clip=music;
        bgm.Play();
    }


}


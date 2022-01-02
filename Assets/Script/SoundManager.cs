using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    public static SoundManager m_instance;
    
    public AudioSource AudioSource; //Drag a reference to the audio source which will play the music.
    
    private AudioSource Farm;
    private int randomX; //Drag a reference to the audio source which will play the music.

    public AudioClip AmbienceSong;
    public AudioClip WaveSong;
    
    public AudioClip[] FarmSong;
    public AudioClip[] CowsSong;
    public AudioClip[] ShotsSong;
    public AudioClip[] ExplSong;
    public AudioClip[] FenceDamageSong;
    public AudioClip[] FenceDestroySong;
    public AudioClip[] FieldDamageSong;
    public AudioClip[] FieldDestroySong;
    
    public enum SoundFX {
        Farm,
        Cows,
        Shots,
        Expl,
        FenceDamage,
        FenceDestroy,
        FieldDamage,
        FieldDestroy
    }

    void Awake() {
        if (m_instance != null && m_instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            m_instance = this;
        }
        Farm = GetComponent<AudioSource>();
        PlayAmbienceSound();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Farm.isPlaying) {
            randomX = Random.Range(0,500);
            if (randomX<=1) {
                PlaySoundFX(Farm, SoundFX.Farm);
            }
        }
    }

    public void PlayAmbienceSound() {
        AudioSource.loop = true;
        if (AmbienceSong != null) {
            AudioSource.Stop();
            AudioSource.volume = 0.7f;
            AudioSource.clip = AmbienceSong;
            AudioSource.Play();
        }
    }
    
    public void PlayWaveSound() {
        AudioSource.loop = true;
        if (WaveSong != null) {
            AudioSource.Stop();
            AudioSource.volume = 0.1f;
            AudioSource.clip = WaveSong;
            AudioSource.Play();
        }
    }

    public void PlaySoundFX(AudioSource efxSource, SoundFX soundF) {
        // select specific sound effect according to sound effect reference
        AudioClip[] clips = null;
        switch (soundF) {
            case SoundFX.Farm:
                clips = FarmSong;
                break;
            case SoundFX.Cows:
                clips = CowsSong;
                break;
            case SoundFX.Shots:
                clips = ShotsSong;
                break;
            case SoundFX.Expl:
                clips = ExplSong;
                break;
            case SoundFX.FenceDamage:
                clips = FenceDamageSong;
                break;
            case SoundFX.FenceDestroy:
                clips = FenceDestroySong;
                break;
            case SoundFX.FieldDamage:
                clips = FieldDamageSong;
                break;
            case SoundFX.FieldDestroy:
                clips = FieldDestroySong;
                break;
            default:
                Console.WriteLine("NO SOUND EFFECT FOR THIS");
                break;
        }

        if (clips != null) {
            //player random sound effect in library of the sound effect reference
            int randomIndex = Random.Range(0, clips.Length);
            efxSource.clip = clips[randomIndex];
            efxSource.loop = false;
            efxSource.Play();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public bool isMusicEnable = true;
    public bool isFxEnable = true;
    [Range(0, 1)]
    public float musicVolume = 1f;
    [Range(0, 1)]
    public float fxVolume = 1f;
    public AudioClip clearSound;
    public AudioClip moveSound;
    public AudioClip dropSound;
    public AudioClip gameOverSound;
    public AudioClip[] backgroundSound;
    public AudioClip errorSound;
    public AudioSource musicSource;
    public AudioClip[] vocalSounds;
    public AudioClip gameOverClip;
    public IconToggle iconToggleMusic;
    public IconToggle iconToggleFX;
    public AudioClip levelUpClip;

    void Start()
    {
        PlayBackgroundMusic(GetRandomAudioClip(backgroundSound));
    }

    public AudioClip GetRandomAudioClip(AudioClip[] audioClips)
    {
      return audioClips[Random.Range(0, backgroundSound.Length)];
    }

    public void PlayBackgroundMusic(AudioClip audioClip)
    {
        if (!isMusicEnable || !audioClip || !musicSource)
            return;

        musicSource.Stop();

        musicSource.clip = audioClip;
        musicSource.volume = musicVolume;
        musicSource.loop = true;

        musicSource.Play();
    }


    private void UpdateMusic()
    {
        if (musicSource.isPlaying != isMusicEnable)
            if (isMusicEnable)
                PlayBackgroundMusic(GetRandomAudioClip(backgroundSound));
            else
                musicSource.Stop();
    }


    public void ToggleMusic()
    {
        isMusicEnable = !isMusicEnable;
        if (isMusicEnable)
            iconToggleMusic.ToggleIcon(isMusicEnable);
        UpdateMusic();
    }

    public void ToggleFX()
    {
        isFxEnable = !isFxEnable;
        if (isFxEnable)
            iconToggleFX.ToggleIcon(isFxEnable);
    }


}

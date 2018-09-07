using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    #region Singleton
    static private SoundManager _instance;
    static public SoundManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("SoundManager", typeof(SoundManager)).GetComponent<SoundManager>();
                DontDestroyOnLoad(_instance);
            }
            return _instance;
        }
    }
    #endregion

    public enum BGMType
    {
        BENSOUND_CUTE,
        SAD_UKULELE_SONG,
        NONE
    }

    public enum SFXType
    {
        CLICK
    }
    public List<AudioClip> bgmClips = new List<AudioClip>();
    public AudioClip clickClip;

    public AudioSource bgmSource;

    private BGMType currentBGMType = BGMType.NONE;


    private void Awake()
    {
        bgmSource = new GameObject("BGMAudioSource", typeof(AudioSource)).GetComponent<AudioSource>();
        bgmSource.transform.parent = transform;
        bgmSource.transform.localPosition = Vector3.zero;
        bgmSource.playOnAwake = false;
        bgmSource.loop = true;

        bgmClips.Add(Resources.Load<AudioClip>("Music/BensoundCute"));
        bgmClips.Add(Resources.Load<AudioClip>("Music/SadUkuleleSong"));

        clickClip = Resources.Load<AudioClip>("SFX/Click_Soft_01");
        //buttonPressClip = Resources.Load<AudioClip>("Sounds/SFX/Other SFX/Button Press");

    }

    public void InvertSound()
    {
        AudioListener.volume = 1f - AudioListener.volume;
    }

    public void PlayBackgroundMusic(BGMType bgType, float volume)
    {
        if (currentBGMType == bgType)
            return;

        currentBGMType = bgType;
        bgmSource.clip = bgmClips[(int)bgType];
        bgmSource.volume = volume;
        bgmSource.Play();
    }

    public void PlaySFX(SFXType sfxType, float volume)
    {
        if (sfxType == SFXType.CLICK)
            AudioSource.PlayClipAtPoint(clickClip, Camera.main.transform.position, volume);
    }
}
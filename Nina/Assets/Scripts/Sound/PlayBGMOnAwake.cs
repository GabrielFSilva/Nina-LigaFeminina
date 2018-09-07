using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBGMOnAwake : MonoBehaviour {

    public SoundManager.BGMType bgm;
    public float volume = 1f;

    private void Awake()
    {
        SoundManager.instance.PlayBackgroundMusic(bgm, volume);
    }
}

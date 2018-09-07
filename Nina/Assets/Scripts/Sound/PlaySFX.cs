using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySFX : MonoBehaviour {

    public SoundManager.SFXType sfx;
    public float volume = 1f;

    public void Play()
    {
        SoundManager.instance.PlaySFX(sfx, volume);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour {

    SoundManager soundManager;

    public Image iconImage;
    public Sprite soundSprite;
    public Sprite muteSprite;
    
	void Start () {
        soundManager = SoundManager.instance;
        UpdateIcon();
    }
	
	void UpdateIcon () {
        if (AudioListener.volume >= 0.95f)
            iconImage.sprite = soundSprite;
        else
            iconImage.sprite = muteSprite;
	}

    public void InvertAudio()
    {
        soundManager.InvertSound();
        UpdateIcon();
    }
}

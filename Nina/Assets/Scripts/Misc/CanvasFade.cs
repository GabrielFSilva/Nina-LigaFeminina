using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasFade : MonoBehaviour
{
    #region Events
    public event Action<CanvasFade, bool> OnFadeBegin;
    public event Action<CanvasFade, bool> OnFadeEnd;
    public event Action<CanvasFade, float> OnFadeUpdate;
    #endregion
   
    [SerializeField]
    private Image fadeImage;


    #region Properties
    private Color fadeColor = Color.black;
    public Color FadeColor
    {
        get { return fadeColor; }
        set {
            fadeColor.r = value.r;
            fadeColor.g = value.g;
            fadeColor.b = value.b;
        }
    }
    private float progress = 0f;
    public float Progress {
        get { return progress; }
    }
    private bool fading = false;
    public bool Fading {
        get { return fading; }
    }
    #endregion

    void Awake () {
        // Try to find the Image Component
        if (fadeImage == null)
            fadeImage = GetComponent<Image>();
        if (fadeImage == null)
            Debug.LogError("No Image found. This Script requires a Image Component");
	}

    // Change the Fade alpha
    public void ChangeFadeAlpha(float alpha)
    {
        fadeColor.a = alpha;
        fadeImage.color = fadeColor;
    }


    #region FadeCalls
    // Fade Calls
    public void Fade(bool fadeIn, float duration = 1f, float endDelay = 0f)
    {
        StartCoroutine(FadeImage(fadeIn, duration, endDelay));
    }

    public void FadeIn(float duration = 1f, float endDelay = 0f)
    {
        StartCoroutine(FadeImage(true, duration, endDelay));
    }

    public void FadeOut(float duration = 1f, float endDelay = 0f)
    {
        StartCoroutine(FadeImage(false, duration, endDelay));
    }
    #endregion

    // Fade Function
    IEnumerator FadeImage(bool fadeIn, float duration, float endDelay)
    {
        fading = true;
        // Begin Action
        if (OnFadeBegin != null)
            OnFadeBegin(this, fadeIn);

        // Counter from 0 to 1
        for (progress = 0f; progress < 1f; progress += Time.deltaTime / duration)
        {
            if (fadeIn)
                ChangeFadeAlpha(1f - progress);
            else
                ChangeFadeAlpha(progress);

            // Update Action
            if (OnFadeUpdate != null)
                OnFadeUpdate(this, progress);
            yield return null;
        }

        // Another Update to avoid iteration errors
        if (fadeIn)
            ChangeFadeAlpha(0f);
        else
            ChangeFadeAlpha(1f);

        // Addition delay after the fade
        if (endDelay > 0.001f)
            yield return new WaitForSeconds(endDelay);

        fading = false;
        // End Action
        if (OnFadeEnd != null)
            OnFadeEnd(this, fadeIn);
    }
}

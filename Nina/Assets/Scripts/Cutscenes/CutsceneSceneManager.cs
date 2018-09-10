using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CutsceneSceneManager : MonoBehaviour {

    public CanvasFade fade;
    public Cutscenes.CutsceneManager cutsceneManager;
    public string nextScene;
    // Use this for initialization
    void Start()
    {
        fade.FadeIn();
        fade.OnFadeEnd += FadeEnd;
        cutsceneManager.OnFinished += CutsceneFinished;
    }

    private void FadeEnd(CanvasFade arg1, bool isFadeIn)
    {
        if (isFadeIn)
            cutsceneManager.Begin();
        else
            SceneManager.LoadScene(nextScene);
    }

    private void CutsceneFinished()
    {
        fade.FadeOut();
    }
    
}

    
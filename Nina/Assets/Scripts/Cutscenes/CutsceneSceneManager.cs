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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
        if (Input.GetKeyDown(KeyCode.M))
            SceneManager.LoadScene("Menu");
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
            SceneManager.LoadScene("Level2");
        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
            SceneManager.LoadScene("Level3");
        if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
            SceneManager.LoadScene("CutsceneTest");
    }
    
}

    
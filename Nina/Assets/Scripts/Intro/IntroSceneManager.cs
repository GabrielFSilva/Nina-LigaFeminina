using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSceneManager : MonoBehaviour
{
    public CanvasFade fade;
    private string nextScene;

    void Start()
    {
        fade.FadeIn(1f, 2f);
        fade.OnFadeEnd += Fade_OnFadeEnd;
    }

    private void Fade_OnFadeEnd(CanvasFade arg1, bool arg2)
    {
        if (arg2)
            fade.FadeOut();
        else
            SceneManager.LoadScene("Menu");
    }

    private void Update()
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
    public void OpenScene(string scene)
    {
        if (fade.Fading)
            return;
        nextScene = scene;
        fade.OnFadeEnd += ChangeScene;
        fade.FadeOut(1f, 0.5f);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void ChangeScene(CanvasFade obj, bool fadeIn)
    {
        SceneManager.LoadScene(nextScene);
    }
}

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
        fade.FadeIn(1f, 3f);
        fade.OnFadeEnd += Fade_OnFadeEnd;
    }

    private void Fade_OnFadeEnd(CanvasFade arg1, bool arg2)
    {
        if (arg2)
            fade.FadeOut();
        else
            SceneManager.LoadScene("Menu");
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

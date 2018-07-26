using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    public CanvasFade fade;
    private string nextScene;

    void Start()
    {
        fade.OnFadeEnd += ChangeScene;
    }

    public void OpenScene(string scene)
    {
        nextScene = scene;
        fade.FadeOut(1f,0.5f);
    }

    private void ChangeScene(CanvasFade obj)
    {
        SceneManager.LoadScene(nextScene);
    }
}

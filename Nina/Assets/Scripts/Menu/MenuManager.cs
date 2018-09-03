using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    public CanvasFade fade;
    private string nextScene;

    public GameObject mainMenuPanel;
    public GameObject creditsPanel;

    void Start()
    {
        fade.FadeIn();
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
        fade.FadeOut(1f,0.5f);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void ChangeScene(CanvasFade obj, bool fadeIn)
    {
        SceneManager.LoadScene(nextScene);
    }

    public void CreditsClicked()
    {
        mainMenuPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void BackButtonClicked()
    {
        mainMenuPanel.SetActive(true);
        creditsPanel.SetActive(false);
    }
}

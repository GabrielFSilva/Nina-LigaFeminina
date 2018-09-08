using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2Manager : MonoBehaviour {

    public CanvasFade fade;
    public Player player;

    public CustomTrigger2D doorToNextLevel;
    public string sceneToLoad;
    private bool changingScene = false;
    // Use this for initialization
    void Start()
    {
        fade.OnFadeEnd += FadeFinished;
        fade.FadeIn();
        doorToNextLevel.OnCustomTriggerEnter2D += DoorTriggered;
        player.OnDied += OnPlayerDied;
    }

    private void OnPlayerDied(Player obj)
    {
        changingScene = true;
        sceneToLoad = "Level2";
        fade.FadeOut(2f, 1f);
    }

    private void FadeFinished(CanvasFade arg1, bool arg2)
    {
        if (!arg2)
            SceneManager.LoadScene(sceneToLoad);
    }

    private void DoorTriggered(Collider2D obj)
    {
        if (obj.gameObject.layer == LayerMask.NameToLayer("Player") && !changingScene)
        {
            doorToNextLevel.OnCustomTriggerEnter2D -= DoorTriggered;
            changingScene = true;
            sceneToLoad = "Level3";
            fade.FadeOut(2f, 1f);
        }
    }
    public void ExitButtonClicked()
    {
        if (!changingScene)
        {
            doorToNextLevel.OnCustomTriggerEnter2D -= DoorTriggered;
            changingScene = true;
            sceneToLoad = "Menu";
            fade.FadeOut(2f, 1f);
        }
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
        if (Input.GetKeyDown(KeyCode.M))
            SceneManager.LoadScene("Menu");
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
            SceneManager.LoadScene("Level2");
        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
            SceneManager.LoadScene("Level3");
    }
}

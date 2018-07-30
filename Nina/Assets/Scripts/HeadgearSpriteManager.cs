using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HeadgearSpriteManager : MonoBehaviour
{
    public enum HeadAcessoryType
    {
        NOTHING,
        BLUE,
        RED,
        GREEN
    }

    public int activeHeadgearIndex;
    public List<Image> headgearImages;
    public CanvasFade fade;
    private string nextScene;

    // Use this for initialization
    void Start () {
        fade.FadeIn();
    }
	
	// Update is called once per frame
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

    public void ChangeHeadgear(int direction)
    {
        headgearImages[activeHeadgearIndex].gameObject.SetActive(false);
        activeHeadgearIndex += direction;

        if (activeHeadgearIndex < 0)
            activeHeadgearIndex = headgearImages.Count - 1;
        else if (activeHeadgearIndex == headgearImages.Count)
            activeHeadgearIndex = 0;

        headgearImages[activeHeadgearIndex].gameObject.SetActive(true);
    }

    public void OpenScene(string scene)
    {
        if (fade.Fading)
            return;
        nextScene = scene;
        fade.OnFadeEnd += ChangeScene;
        fade.FadeOut(1f, 0.5f);
    }

    private void ChangeScene(CanvasFade obj, bool fadeIn)
    {
        SceneManager.LoadScene(nextScene);
    }
}

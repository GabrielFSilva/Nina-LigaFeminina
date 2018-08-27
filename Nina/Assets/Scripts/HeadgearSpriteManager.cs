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

    public int accessorieIndex;
    public List<Sprite> accessoriesSprites;
    public Image accessorieImage;
    public int baseIndex;
    public List<Sprite> baseSprites;
    public Image baseImage;

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
        if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
            SceneManager.LoadScene("CutsceneTest");
    }

    public void ChangeAccessorie(int direction)
    {
        accessorieIndex += direction;

        if (accessorieIndex < 0)
            accessorieIndex = accessoriesSprites.Count - 1;
        else if (accessorieIndex == accessoriesSprites.Count)
            accessorieIndex = 0;

        accessorieImage.sprite = accessoriesSprites[accessorieIndex];

        if (accessorieImage.sprite)
            accessorieImage.color = Color.white;
        else
            accessorieImage.color = new Color(1f, 1f, 1f, 0f);
    }

    public void ChangeBase(int direction)
    {
        baseIndex += direction;

        if (baseIndex < 0)
            baseIndex = baseSprites.Count - 1;
        else if (baseIndex == baseSprites.Count)
            baseIndex = 0;

        baseImage.sprite = baseSprites[baseIndex];
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

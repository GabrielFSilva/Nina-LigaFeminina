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

    public int accessoryIndex;
    public List<Sprite> accessoriesSprites;
    public Image accessoryImage;
    public int baseIndex;
    public List<Sprite> baseSprites;
    public Image baseImage;

    public CanvasFade fade;

    private string nextScene;

    // Use this for initialization
    void Start () {
        fade.FadeIn();
    }
	
	public void ChangeAccessorie(int direction)
    {
        accessoryIndex += direction;

        if (accessoryIndex < 0)
            accessoryIndex = accessoriesSprites.Count - 1;
        else if (accessoryIndex == accessoriesSprites.Count)
            accessoryIndex = 0;

        accessoryImage.sprite = accessoriesSprites[accessoryIndex];

        if (accessoryImage.sprite)
            accessoryImage.color = Color.white;
        else
            accessoryImage.color = new Color(1f, 1f, 1f, 0f);
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

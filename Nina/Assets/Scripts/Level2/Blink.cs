using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
    public List<SpriteRenderer> sprites;
    public float alphaWhenTransparent = 0f;
    public float timeOn = 0.2f;
    public float timeOff = 0.2f;

    public bool blinking = false;

    public void StartBlink()
    {
        if (!blinking)
        {
            blinking = true;
            StartCoroutine(Blinking());
        }
    }

    public void StopBlink()
    {
        if (blinking)
        {
            blinking = false;
            StopAllCoroutines();
            UpdateBlink(false);
        }
    }

    private void UpdateBlink(bool transparent)
    {
        foreach (SpriteRenderer sprite in sprites)
        {
            Color c = sprite.color;
            if (transparent)
            {
                c.a = alphaWhenTransparent;
            }
            else
            {
                c.a = 1f;
            }
            sprite.color = c;
        }
    }
    IEnumerator Blinking()
    {
        while (true)
        {
            UpdateBlink(true);
            yield return new WaitForSeconds(timeOn);
            UpdateBlink(false);
            yield return new WaitForSeconds(timeOff);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class IconToggle : MonoBehaviour
{
    public Sprite iconTrueSprite;
    public Sprite iconFalseSprite;
    public bool isDefaultIconState=true;

    private Image image;

    void Start()
    {
        image = GetComponent<Image>();
        image.sprite = isDefaultIconState ? iconTrueSprite : iconTrueSprite;
    }


    public void ToggleIcon(bool state)
    {
        if (!image || !iconTrueSprite || !iconFalseSprite)
            return;
        
        image.sprite = state ? iconTrueSprite : iconTrueSprite;
    }
}

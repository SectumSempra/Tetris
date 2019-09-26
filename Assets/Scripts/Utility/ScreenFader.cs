using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MaskableGraphic))]
public class ScreenFader : MonoBehaviour
{
    public float startAlpha = 1f;
    public float targetAlpha = 0f;
    public float delay = 0f;
    public float timeToFade = 1f;

    private float inc;
    private float currentAlpha;
    private MaskableGraphic graphic;
    private Color orginalColor;


    void Start()
    {
        graphic = GetComponent<MaskableGraphic>();
        orginalColor = graphic.color;
        currentAlpha = startAlpha;
        graphic.color = new Color(orginalColor.r, orginalColor.g, orginalColor.b, currentAlpha);

        inc = ((targetAlpha - startAlpha) / timeToFade) * Time.deltaTime;

        StartCoroutine(FadeRoutine());
    }

    IEnumerator FadeRoutine()
    {
        yield return new WaitForSeconds(delay);
        while(Mathf.Abs(targetAlpha-startAlpha)>0.01f)
        {
            yield return new WaitForEndOfFrame();
            currentAlpha += inc;
            graphic.color = new Color(orginalColor.r, orginalColor.g, orginalColor.b, currentAlpha);

        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlphaFader : MonoBehaviour
{
    [SerializeField] private Image[] _Images;
    [SerializeField] private TMPro.TextMeshProUGUI[] _Fonts;

    private Color[] o_Images;
    private Color[] o_Fonts;

    private IEnumerator _EAlphaFade;

    private void Awake()
    {
        o_Images = new Color[_Images.Length];
        for (int i = 0; i < o_Images.Length; ++i)
        {
            o_Images[i] = _Images[i].color;
        }

        o_Fonts = new Color[_Fonts.Length];
        for(int i = 0; i < o_Fonts.Length; ++i)
        {
            o_Fonts[i] = _Fonts[i].color;
        }
    }
    public void SetAlpha(float alpha)
    {
        Color targetColor;

        for (int i = 0; i < _Images.Length; ++i)
        {
            targetColor = new Color(o_Images[i].r, o_Images[i].g, o_Images[i].b, alpha);

            _Images[i].color = targetColor;
        }
        for (int i = 0; i < _Fonts.Length; ++i)
        {
            targetColor = new Color(o_Fonts[i].r, o_Fonts[i].g, o_Fonts[i].b, alpha);

            _Fonts[i].color = targetColor;
        }
    }
    public void AlphaFade(float alpha, float time)
    {
        if (_EAlphaFade != null)
        {
            StopCoroutine(_EAlphaFade);
        }
        StartCoroutine(_EAlphaFade = EAlphaFade(alpha, time));
    }
    private IEnumerator EAlphaFade(float alphaPercent, float time)
    {
        for (float wait = 0; wait < time; wait += Time.deltaTime)
        {
            float ratio = Mathf.Min(wait, time) / time;

            Color targetColor;

            for (int i = 0; i < _Images.Length; ++i)
            {
                targetColor = new Color(o_Images[i].r, o_Images[i].g, o_Images[i].b, o_Images[i].a * alphaPercent);

                _Images[i].color = Color.Lerp(_Images[i].color, targetColor, ratio);
            }
            for (int i = 0; i < _Fonts.Length; ++i)
            {
                targetColor = new Color(o_Fonts[i].r, o_Fonts[i].g, o_Fonts[i].b, o_Fonts[i].a * alphaPercent);

                _Fonts[i].color = Color.Lerp(_Fonts[i].color, targetColor, ratio);
            }
            yield return null;
        }
        _EAlphaFade = null;
    }
}

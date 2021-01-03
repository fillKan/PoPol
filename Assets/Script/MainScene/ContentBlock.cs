using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ContentBlock : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private ContentBlockControler _Controler;
    [SerializeField] private Image _Image;
    [SerializeField] private float _AnimationTime;
    [SerializeField] private Vector2 _TranslatePosition;
    
    private float _StartScale;
    private bool  _IsOpend;

    private IEnumerator _Animation;
    private IEnumerator _EFade;

    private void Awake()
    {
        _IsOpend = false;
        _StartScale = transform.localScale.x;
    }
    public void Fade(float alpha, float time)
    {
        if (_EFade != null)
        {
            StopCoroutine(_EFade);
        }
        StartCoroutine(_EFade = EFade(alpha, time));
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (_Animation != null)
        {
            StopCoroutine(_Animation);
        }
        StartCoroutine(_Animation = Animation(!_IsOpend));
        _Controler.Translate(_TranslatePosition, _AnimationTime);
    }
    private IEnumerator Animation(bool open)
    {
        _IsOpend = open;
        Vector2 targetScale;

        if (open)
        {
            targetScale = Vector2.one;
            _Controler.FadeCall(0.4f, this, _AnimationTime);
        }
        else
        {
            targetScale = _StartScale * Vector2.one;
            _Controler.FadeCall(1f, this, _AnimationTime);
        }
        for (float i = 0f; i < _AnimationTime; i += Time.deltaTime)
        {
            i = Mathf.Min(1f, i);

            float ratio = i / _AnimationTime;
            transform.localScale = Vector2.Lerp(transform.localScale, targetScale, ratio);

            _Controler.LayoutRebuild();
            yield return null;
        }
        _Animation = null;
    }
    private IEnumerator EFade(float alpha, float time)
    {
        Color fadeColor = new Color(1, 1, 1, alpha);

        for (float i = 0f; i < time; i += Time.deltaTime)
        {
            float ratio = i / time;
            _Image.color = Color.Lerp(_Image.color, fadeColor, ratio);

            yield return null;
        }
        _EFade = null;
    }
}

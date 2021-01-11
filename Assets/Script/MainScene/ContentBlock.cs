using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ContentBlock : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private ContentBlockControler _Controler;
    [SerializeField] private AlphaFader _AlphaFader;
    [SerializeField] private AlphaFader _IntroductionAlphaFader;
    [SerializeField] private float _AnimationTime;
    [SerializeField] private Vector2 _TranslatePosition;
    [SerializeField] private Color _BackgroundColor;
    [SerializeField] private int _AttachSceneIndex;
    
    private float _StartScale;
    private bool  _IsOpend;

    private IEnumerator _Animation;

    private void Awake()
    {
        _IsOpend = false;
        _StartScale = transform.localScale.x;
    }
    private void Start()
    {
        _IntroductionAlphaFader.SetAlpha(0f);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_IsOpend)
            {
                _Controler.AnimationCall(this, _TranslatePosition, _AnimationTime, 1f, 1f, 0.4f, 0.4f);

                ColorChanger.Instance.ColorChange(Color.white, _AnimationTime);
            }
            else
            {
                _Controler.AnimationCall(this, _TranslatePosition, _AnimationTime, 0.05f, 1f, 0.4f, 1f);

                ColorChanger.Instance.ColorChange(_BackgroundColor, _AnimationTime);
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            if (_IsOpend)
            {
                SceneLoader.Instance.SceneLoad(_AttachSceneIndex);
            }
        }
    }
    public void PlayAnimation(float time, float alpha, float scale)
    {
        if (_Animation != null)
        {
            StopCoroutine(_Animation);
        }
        StartCoroutine(_Animation = Animation(time, alpha, scale));

        _AlphaFader.AlphaFade(alpha, time);
    }
    private IEnumerator Animation(float time, float alpha, float scale)
    {
        _IsOpend = (scale > _StartScale);

        if (_IsOpend)
        {
            _IntroductionAlphaFader.AlphaFade(1f, time);
        }
        else
        {
            _IntroductionAlphaFader.AlphaFade(0f, time);
        }
        Vector2 targetScale = Vector2.one * scale;

        Color fadeColor = new Color(1, 1, 1, alpha);

        for (float i = 0f; i < time; i += Time.deltaTime)
        {
            float ratio = Mathf.Min(time, i) / time;

            transform.localScale = Vector2.Lerp(transform.localScale, targetScale, ratio);

            _Controler.LayoutRebuild();
            yield return null;
        }
        _Animation = null;
    }
}

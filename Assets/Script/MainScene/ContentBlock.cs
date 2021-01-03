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

    private void Awake()
    {
        _IsOpend = false;
        _StartScale = transform.localScale.x;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (_IsOpend)
        {
            _Controler.AnimationCall(this, _TranslatePosition, _AnimationTime, 1f, 1f, 0.4f, 0.4f);
        }
        else
            _Controler.AnimationCall(this, _TranslatePosition, _AnimationTime, 0.05f, 1f, 0.4f, 1f);
    }
    public void PlayAnimation(float time, float alpha, float scale)
    {
        if (_Animation != null)
        {
            StopCoroutine(_Animation);
        }
        StartCoroutine(_Animation = Animation(time, alpha, scale));
    }
    private IEnumerator Animation(float time, float alpha, float scale)
    {
        _IsOpend = (scale > _StartScale);

        Vector2 targetScale = Vector2.one * scale;

        Color fadeColor = new Color(1, 1, 1, alpha);

        for (float i = 0f; i < time; i += Time.deltaTime)
        {
            float ratio = Mathf.Min(time, i) / time;

            transform.localScale = Vector2.Lerp(transform.localScale, targetScale, ratio);
            //_Image.color = Color.Lerp(_Image.color, fadeColor, ratio);

            _Controler.LayoutRebuild();
            yield return null;
        }
        _Animation = null;
    }
}

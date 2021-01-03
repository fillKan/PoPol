using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ContentBlock : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private float _AnimationTime;
    
    private float _StartScale;
    private bool _IsOpend;

    private IEnumerator _Animation;

    private void Awake()
    {
        _IsOpend = false;
        _StartScale = transform.localScale.x;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (_Animation != null)
        {
            StopCoroutine(_Animation);
        }
        StartCoroutine(_Animation = Animation(!_IsOpend));
    }
    private IEnumerator Animation(bool open)
    {
        _IsOpend = open;
        Vector2 targetScale;

        if (open)
        {
            targetScale = Vector2.one;
        }
        else
        {
            targetScale = _StartScale * Vector2.one;
        }
        for (float i = 0f; i < _AnimationTime; i += Time.deltaTime)
        {
            i = Mathf.Min(1f, i);

            float ratio = i / _AnimationTime;
            transform.localScale = Vector2.Lerp(transform.localScale, targetScale, ratio);

            LayoutRebuilder.ForceRebuildLayoutImmediate(transform.parent.GetComponent<RectTransform>());

            yield return null;
        }
        _Animation = null;
    }
}

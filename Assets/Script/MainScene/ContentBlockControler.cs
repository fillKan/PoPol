using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentBlockControler : MonoBehaviour
{
    [SerializeField] private RectTransform _RectTransform;
    [SerializeField] private ContentBlock[] _ContentBlocks;

    private IEnumerator _ETranslate;

    public void LayoutRebuild()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(_RectTransform);
    }
    public void AnimationCall(ContentBlock target, Vector2 translation, float time, float otherAlpha, float targetAlpha, float otherScale, float targeScale)
    {
        for (int i = 0; i < _ContentBlocks.Length; ++i)
        {
            if (!_ContentBlocks[i].Equals(target))
            {
                _ContentBlocks[i].PlayAnimation(time, otherAlpha, otherScale);
            }
            else
            {
                _ContentBlocks[i].PlayAnimation(time, targetAlpha, targeScale);
            }
        }
        if (_ETranslate != null)
        {
            StopCoroutine(_ETranslate);
        }
        StartCoroutine(_ETranslate = ETranslate(translation, time));
    }
    private IEnumerator ETranslate(Vector2 targetPos, float time)
    {
        for (float i = 0f; i < time; i += Time.deltaTime)
        {
            float ratio = Mathf.Min(time, i) / time;

            transform.localPosition = Vector2.Lerp(transform.localPosition, targetPos, ratio);
            yield return null;
        }
        _ETranslate = null;
    }
}

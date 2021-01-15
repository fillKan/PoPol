using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ContentAnim", menuName = "Scriptable/ContentAnim")]
public class ContentAnim : ScriptableObject
{
    [Header("Open Animation")]
    [Range(0f, 1f)]
    [SerializeField] private float _OpenAlpha;
    [SerializeField] private float _OpenScale;

    [Header("Default Animation")]
    [Range(0f, 1f)]
    [SerializeField] private float _DefaultAlpha;
    [SerializeField] private float _DefaultScale;

    [Header("Close Animation")]
    [Range(0f, 1f)]
    [SerializeField] private float _CloseAlpha;
    [SerializeField] private float _CloseScale;

    public void GetAnim(ContentBlock.AnimationType anim, out float alpha, out float scale)
    {
        switch (anim)
        {
            case ContentBlock.AnimationType.Open:
                alpha = _OpenAlpha;
                scale = _OpenScale;
                break;
            case ContentBlock.AnimationType.Default:
                alpha = _DefaultAlpha;
                scale = _DefaultScale;
                break;
            case ContentBlock.AnimationType.Close:
                alpha = _CloseAlpha;
                scale = _CloseScale;
                break;
            default:
                alpha = 1f;
                scale = 1f;
                break;
        }
    }
}

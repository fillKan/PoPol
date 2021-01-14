using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * BGRect.w, BGRect.h
 *  |_____________  |__
 *                ↓    ↓
 * 1 : 1.1 : 1.5 : 1.8 : 2
 * ↑    ↑     ↑____________
 * |    |_______           |
 * |            |          |
 * FontSize, FontRect.w, FontRect.h
 */
public class Subtitle : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _Text;
    [SerializeField] private RectTransform _BackGroundRect;

    [Space(10f)][TextArea(2, 4)]
    [SerializeField] private string[] _Subtitles;

    private IEnumerator _SubtitleCollection;

    private void Awake()
    {
        _SubtitleCollection = _Subtitles.GetEnumerator();
        _SubtitleCollection.MoveNext();

        _Text.text = (string)_SubtitleCollection.Current;

        int blankCount = _Text.text.Split(' ').Length - 1;

        _Text.rectTransform.sizeDelta = new Vector2(_Text.fontSize * _Text.text.Length * 1.1f, _Text.fontSize * 1.5f);
        _BackGroundRect.sizeDelta = new Vector2(_Text.fontSize * _Text.text.Length * 1.1f, _Text.fontSize * 2f);
    }
}

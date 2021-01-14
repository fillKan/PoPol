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
public class SubtitleWriter : Singleton<SubtitleWriter>
{
    [SerializeField] private int _Page = 0;

    [SerializeField] private TMPro.TextMeshProUGUI _Text;
    [SerializeField] private RectTransform _BackGroundRect;

    [Space(10f)]
    [SerializeField] private SubtitleSet[] _SubtitleSets;

    public void TurnTheNextPage() 
    {
        if (_Page < _SubtitleSets.Length - 1)
        {
            _Page++;
        }
    }
    public void TurnThePrevPage() 
    {
        if (_Page > 0)
        {
            _Page--;
        }
    }

    public void WriteSubtitle()
    {
        float  fontSize = _Text.fontSize;
        string subtitle = _SubtitleSets[_Page].NextSubtitle();

        int maxLength = 0;
        var subtitles = subtitle.Split('\n');
        for (int i = 0; i < subtitles.Length; ++i)
        {
            if (maxLength < subtitles[i].Length)
            {
                maxLength = subtitles[i].Length;
            }
        }
        int newLine = subtitles.Length;
        _Text.text = subtitle;

        _Text.rectTransform.sizeDelta
            = new Vector2(fontSize * maxLength * 1.1f, fontSize * newLine * 1.5f);

        _BackGroundRect.sizeDelta
            = new Vector2(fontSize * maxLength * 1.1f, fontSize * newLine * 1.8f);
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SubtitleSet", menuName = "Scriptable/SubtitleSet")]
public class SubtitleSet : ScriptableObject
{
    private int _Index = 0;

    [TextArea(3, 3)]
    [SerializeField] private string[] _Subtitles;

    public void Reset()
    {
        _Index = 0;
    }
    public string[] SubtitleAll()
    {
        return _Subtitles;
    }
    public string NextSubtitle()
    {
        if (_Index < _Subtitles.Length)
        {
            return _Subtitles[_Index++];
        }
        else
            return string.Empty;
    }
}

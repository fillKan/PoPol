using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Presenter : MonoBehaviour
{
    [SerializeField] private AlphaFader _AlphaFader;
    [SerializeField] private Image _Image;

    [Header("Presentation Sprite")]
    [SerializeField] private Sprite[] _Pages;
    private int _Index;

    private void Awake()
    {
        _Index = 1;
    }
    private void Start()
    {
        _AlphaFader.SetAlpha(0f);
        _AlphaFader.AlphaFadeRegular(1f, 1.5f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_Index < _Pages.Length)
            {
                _Image.sprite = _Pages[_Index++];
            }
        }
    }
}

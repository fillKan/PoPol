using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Presenter : MonoBehaviour
{
    [SerializeField] private Image _Image;

    [Header("Presentation Sprite")]
    [SerializeField] private Sprite[] _Pages;
    private int _Index;

    private void Awake()
    {
        _Index = 0;
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

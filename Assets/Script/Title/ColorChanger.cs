using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private float _Duration;
    [SerializeField] private float _ChangeTime;

    [Space()]
    [SerializeField] private Color[] _Colors;

    private int _ColorIndex;

    private void Awake()
    {
        _ColorIndex = 0;

        StartCoroutine(ColorChange());
    }
    private IEnumerator ColorChange()
    {
        while (enabled)
        {
            Color cameraColor = Camera.main.backgroundColor;

            for (float i = 0; i < _ChangeTime; i += Time.deltaTime)
            {
                float ratio = i / _ChangeTime;

                Camera.main.backgroundColor = Color.Lerp(cameraColor, _Colors[_ColorIndex], ratio);

                yield return null;
            }
            if (++_ColorIndex >= _Colors.Length)
            {
                _ColorIndex = 0;
            }
            yield return new WaitForSeconds(_Duration);
        }
    }
}

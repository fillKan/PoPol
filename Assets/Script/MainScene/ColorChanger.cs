using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : Singleton<ColorChanger>
{
    private IEnumerator _EColorChange;

    public void ColorChange(Color color, float time)
    {
        if (_EColorChange != null)
        {
            StopCoroutine(_EColorChange);
        }
        StartCoroutine(_EColorChange = EColorChange(color, time));
    }
    private IEnumerator EColorChange(Color color, float time)
    {
        for (float i = 0; i < time; i += Time.deltaTime)
        {
            float ratio = Mathf.Min(i, time) / time;

            Camera.main.backgroundColor = Color.Lerp(Camera.main.backgroundColor, color, ratio);
            yield return null;
        }
        _EColorChange = null;
    }
}

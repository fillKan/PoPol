using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : Singleton<MainCamera>
{
    private IEnumerator _EShake;
    private Vector2 _OriginPosition;

    public void Shake(float power, float time)
    {
        if (_EShake != null)
        {
            StopCoroutine(_EShake);
            _EShake = null;

            ShakeOverAction();
        }
        StartCoroutine(_EShake = EShake(power, time));
    }
    private void ShakeOverAction()
    {
        transform.position = _OriginPosition;
        transform.Translate(0, 0, -10f);
    }
    private IEnumerator EShake(float power, float time)
    {
        _OriginPosition = transform.position;

        for (float i = 0; i < time; i += Time.deltaTime)
        {
            i = Mathf.Min(1f, i);

            transform.position = _OriginPosition + Random.insideUnitCircle * Mathf.Lerp(power, 0, i / time);
            transform.Translate(0, 0, -10f);

            yield return null;
        }
        ShakeOverAction();
    }
}

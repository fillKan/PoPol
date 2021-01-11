using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : Singleton<MainCamera>
{
    private Coroutine _ShakeRoutine;

    private void Awake()
    {
        _ShakeRoutine = new Coroutine(this);
        _ShakeRoutine.RoutineStopEvent += () => 
        {
            transform.localPosition = Vector2.zero;
            transform.Translate(0, 0, -10f);
        };
    }
    public void Shake(float power, float time)
    {
        _ShakeRoutine.StartRoutine(EShake(power, time));
    }
    private IEnumerator EShake(float power, float time)
    {
        for (float i = 0; i < time; i += Time.deltaTime)
        {
            i = Mathf.Min(i, time);
            power = Mathf.Lerp(power, 0, i / time);

            transform.localPosition = Random.insideUnitCircle * power;
            transform.Translate(0, 0, -10f);

            yield return null;
        }
        _ShakeRoutine.FinshRoutine();
    }
}

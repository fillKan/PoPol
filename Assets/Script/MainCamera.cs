using System.Collections;
using UnityEngine;

public class MainCamera : Singleton<MainCamera>
{
    private Coroutine _ShakeRoutine;
    private Coroutine  _MoveRoutine;

    private void Awake()
    {
        _ShakeRoutine = new Coroutine(this);
        _ShakeRoutine.RoutineStopEvent += () => 
        {
            transform.localPosition = Vector2.zero;
            transform.Translate(0, 0, -10f);
        };
        _MoveRoutine = new Coroutine(this);
    }

    public void Shake(float power, float time)
    {
        if (!_MoveRoutine.IsDuration())
        {
            _ShakeRoutine.StartRoutine(EShake(power, time));
        }
    }
    public void Move(float time, Vector2 start, Vector2 goal, System.Action overAction = null)
    {
        transform.position = start;

        _ShakeRoutine.StopRoutine();
        _MoveRoutine.StartRoutine(EMove(time, goal));

        void OverAction()
        {
            overAction?.Invoke();
            _MoveRoutine.RoutineStopEvent -= OverAction;
        }
        _MoveRoutine.RoutineStopEvent += OverAction;
    }

    // =================== IEnumator =================== //
    private IEnumerator EShake(float power, float time)
    {
        Vector2 startPosition = transform.position;

        for (float i = 0; i < time; i += Time.deltaTime)
        {
            i = Mathf.Min(i, time);
            power = Mathf.Lerp(power, 0, i / time);

            transform.position = startPosition + Random.insideUnitCircle * power;
            transform.Translate(0, 0, -10f);

            yield return null;
        }
        _ShakeRoutine.FinshRoutine();
    }
    private IEnumerator EMove(float time, Vector2 position)
    {
        for (float i = 0f; i < time; i += Time.deltaTime)
        {
            float ratio = Mathf.Min(i, time) / time;

            transform.position = Vector2.Lerp(transform.position, position, ratio);
            transform.Translate(0, 0, -10f);

            yield return null;
        }
        _MoveRoutine.FinshRoutine();
    }
}

using System.Collections;
using UnityEngine;

public class MainCamera : Singleton<MainCamera>
{
    private Camera _MainCamera;

    private Coroutine _ShakeRoutine;
    private Coroutine _ColorRoutine;
    private Coroutine  _MoveRoutine;

    private void Awake()
    {
        Debug.Assert(TryGetComponent(out _MainCamera));

        _ColorRoutine = new Coroutine(this);
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
        _ShakeRoutine.StartRoutine(EShake(power, time));
    }
    public void Move(float time, Vector2 start, Vector2 goal, System.Action overAction = null)
    {
        transform.parent.position = start;
        _MoveRoutine.StartRoutine(EMove(time, goal));

        void OverAction()
        {
            overAction?.Invoke();
            _MoveRoutine.RoutineStopEvent -= OverAction;
        }
        _MoveRoutine.RoutineStopEvent += OverAction;
    }
    public void ColorChange(float time, Color color)
    {
        _ColorRoutine.StartRoutine(EColorChange(time, color));
    }
    // =================== IEnumator =================== //
    private IEnumerator EShake(float power, float time)
    {
        for (float i = 0; i < time; i += Time.deltaTime)
        {
            i = Mathf.Min(i, time);
            power = Mathf.Lerp(power, 0, i / time);

            transform.localPosition = Random.insideUnitCircle * power;
            transform.Translate(0, 0, -10f, Space.Self);

            yield return null;
        }
        _ShakeRoutine.FinshRoutine();
    }
    private IEnumerator EMove(float time, Vector2 position)
    {
        for (float i = 0f; i < time; i += Time.deltaTime)
        {
            float ratio = Mathf.Min(i, time) / time;

            transform.parent.position = Vector2.Lerp(transform.position, position, ratio);
            transform.parent.Translate(0, 0, -10f);

            yield return null;
        }
        _MoveRoutine.FinshRoutine();
    }
    private IEnumerator EColorChange(float time, Color color)
    {
        for (float i = 0; i < time; i += Time.deltaTime)
        {
            float ratio = Mathf.Min(i, time) / time;

            _MainCamera.backgroundColor = Color.Lerp(_MainCamera.backgroundColor, color, ratio);
            yield return null;
        }
        yield return null;
    }
}

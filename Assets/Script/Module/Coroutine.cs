using System;
using System.Collections;
using UnityEngine;

#region Comment
/// <summary>
/// <b>유니티에서 제공하는 코루틴 기능의 제어를 보조합니다</b>
/// <para></para>
/// - 하나의 루틴만이 실행되는 것을 보장합니다.
/// <br></br>
/// - 진행중인 루틴이 종료되었는지의 여부를 확인할 수 있습니다.
/// <para></para>
/// <i>IsDuration함수를 사용하기 위해선 진행한 루틴이 
/// <br></br>종료되었을 때 FinshRoutine()함수를 호출해야 합니다.</i>
/// </summary>
#endregion
public class Coroutine
{
    public event Action RoutineStopEvent;

    private MonoBehaviour _User;
    private IEnumerator _RunningRoutine;

    public Coroutine(MonoBehaviour user)
    {
        _User = user;
    }
    public void StartRoutine(IEnumerator routine)
    {
        StopRoutine();

        _User.StartCoroutine(_RunningRoutine = routine);
    }
    public void StopRoutine()
    {
        if (_RunningRoutine != null) {

            _User.StopCoroutine(_RunningRoutine);
        }
        _RunningRoutine = null;

        RoutineStopEvent?.Invoke();
    }
    public void FinshRoutine()
    {
        _RunningRoutine = null;

        RoutineStopEvent?.Invoke();
    }
    public bool IsDuration()
    {
        return _RunningRoutine != null;
    }
}

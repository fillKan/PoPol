using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenceDirector : MonoBehaviour
{
    [SerializeField] private bool _UsingAwakeMove;
    [SerializeField] private Transform _MoveTarget;
    [SerializeField] private Vector2 _TargetPosition;
    [SerializeField] private Vector2 _StartPosition;

    [SerializeField] private bool _UsingColorChanger;

    private Coroutine _ObjectMove;

    private void Awake()
    {
        _ObjectMove = new Coroutine(this);

        if (_UsingAwakeMove)
        {
            if (_MoveTarget == null)
            {
                MainCamera.Instance.Move(2.5f, _StartPosition, _TargetPosition);
            }
            else
            {
                _ObjectMove.StartRoutine(EObjectMove(_TargetPosition, 2.5f));
            }
        };
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex != 0)
        {
            _ObjectMove.StopRoutine();

            if (_UsingColorChanger)
            {
                MainCamera.Instance.ColorChange(2.4f, Color.white);
            }
            if (_UsingAwakeMove)
            {
                MainCamera.Instance.Move(2.5f, _TargetPosition, _StartPosition, 
                    () => { SceneManager.LoadScene(0); });
            }
            else
            {
                SceneManager.LoadScene(0);
            }
        }
    }
    private IEnumerator EObjectMove(Vector2 poistion, float time)
    {
        _MoveTarget.localPosition = _StartPosition;

        for (float i = 0f; i < time; i += Time.deltaTime)
        {
            float ratio = Mathf.Min(i, time) / time;

            _MoveTarget.localPosition = Vector2.Lerp(_MoveTarget.localPosition, poistion, ratio);
            yield return null;
        }
        _ObjectMove.FinshRoutine();
    }
}

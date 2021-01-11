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

    private IEnumerator _ECameraMove;

    private void Awake()
    {

        if (_UsingAwakeMove)
        {
            if (_MoveTarget == null)
            {
                MainCamera.Instance.Move(2.5f, _StartPosition, _TargetPosition);
            }
            else
            {
                _MoveTarget.localPosition = _StartPosition;
            }
            StartCoroutine(_ECameraMove = ECameraMove(_TargetPosition, 2.5f));
        };
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex != 0)
        {
            if (_ECameraMove != null)
            {
                StopCoroutine(_ECameraMove);
            }            
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
    private IEnumerator ECameraMove(Vector2 poistion, float time, Action moveOverAction = null)
    {
        for (float i = 0f; i < time; i += Time.deltaTime)
        {
            float ratio = Mathf.Min(i, time) / time;

            if (_MoveTarget == null)
            {
                transform.position = Vector2.Lerp(transform.position, poistion, ratio);

                transform.Translate(0, 0, -10f);
            }
            else
            {
                _MoveTarget.localPosition = Vector2.Lerp(_MoveTarget.localPosition, poistion, ratio);
            }
            yield return null;
        }
        moveOverAction?.Invoke();

        _ECameraMove = null;
    }
}

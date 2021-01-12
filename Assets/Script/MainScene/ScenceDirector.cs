using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenceDirector : MonoBehaviour
{
    [SerializeField] private bool _UsingAwakeMove;

    [SerializeField] private Vector2 _TargetPosition;
    [SerializeField] private Vector2 _StartPosition;

    [SerializeField] private bool _UsingColorChanger;

    private Coroutine _ObjectMove;

    private void Awake()
    {
        _ObjectMove = new Coroutine(this);

        if (_UsingAwakeMove)
        {
            MainCamera.Instance.Move(2.5f, _StartPosition, _TargetPosition);
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
}

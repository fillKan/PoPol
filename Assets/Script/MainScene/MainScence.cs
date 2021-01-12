using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScence : MonoBehaviour
{
    public const float LoadTime = 1.5f;
    public const float LoadBeforeTime = 1.4f;

    private readonly Vector2 StartTrans = new Vector2(0, 980f);

    [SerializeField] private AlphaFader _ExitText;
    [SerializeField] private RectTransform _ScrollVlew;

    private Coroutine _ScrollVlewTrans;

    private void Awake()
    {
        _ScrollVlewTrans = new Coroutine(this);

        var contents = FindObjectsOfType<ContentBlock>();

        for (int i = 0; i < contents.Length; ++i)
        {
            contents[i].SelectedClickEvent += o =>
            {
                _ExitText.AlphaFade(1f, LoadBeforeTime);

                _ScrollVlewTrans.StartRoutine(ScrollVlewTrans(StartTrans, LoadTime));
                _ScrollVlewTrans.RoutineStopEvent += () =>
                {
                    SceneManager.LoadScene(o.AttachSceneIndex);
                };
            };
        }
        _ScrollVlew.transform.localPosition = StartTrans;

        _ScrollVlewTrans.StartRoutine(ScrollVlewTrans(Vector2.zero, LoadTime));
    }
    private void Start()
    {
        _ExitText.SetAlpha(0f);
    }
    private void Update()
    {
        if (!Application.isEditor)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }

    private IEnumerator ScrollVlewTrans(Vector2 poistion, float time)
    {
        for (float i = 0f; i < time; i += Time.deltaTime)
        {
            float ratio = Mathf.Min(i, time) / time;

            _ScrollVlew.localPosition = Vector2.Lerp(_ScrollVlew.localPosition, poistion, ratio);
            yield return null;
        }
        _ScrollVlewTrans.FinshRoutine();
    }
}

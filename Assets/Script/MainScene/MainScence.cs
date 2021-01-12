using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScence : MonoBehaviour
{
    private const float SceneLoadTime = 2.5f;

    [SerializeField] private TMPro.TextMeshProUGUI _ExitText;
    [SerializeField] private RectTransform _ScrollVlew;

    private Coroutine _SceneLoad;

    private void Start()
    {
        _SceneLoad = new Coroutine(this);

        var contents = FindObjectsOfType<ContentBlock>();

        for (int i = 0; i < contents.Length; ++i)
        {
            contents[i].SelectedClickEvent += o =>
            {
                _SceneLoad.StartRoutine(SceneLoad(o.AttachSceneIndex));
            };
        }
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

    private IEnumerator SceneLoad(int index)
    {
        for (float i = 0f; i < SceneLoadTime; i += Time.deltaTime)
        {
            float ratio = Mathf.Min(i, SceneLoadTime) / SceneLoadTime;

            _ExitText.color = Color.Lerp(_ExitText.color, Color.white, ratio);

            _ScrollVlew.localPosition = Vector2.Lerp(_ScrollVlew.localPosition, new Vector2(0, 1000), ratio);

            yield return null;
        }
        _SceneLoad.FinshRoutine();

        SceneManager.LoadScene(index);
    }
}

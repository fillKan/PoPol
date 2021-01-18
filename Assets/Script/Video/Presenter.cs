using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Presenter : MonoBehaviour
{
    [SerializeField] private AlphaFader _MetalicBG;
    [SerializeField] private AlphaFader _AlphaFader;
    [SerializeField] private Image _Image;

    [Header("Presentation Sprite")]
    [SerializeField] private Sprite[] _Pages;
    private int _Index;

    private void Awake()
    {
        _Index = 1;
    }
    private void Start()
    {
        _AlphaFader.SetAlpha(0f);
        _AlphaFader.AlphaFadeRegular(1f, 1.5f);

        _MetalicBG.SetAlpha(0f);

        SubtitleWriter.Instance.PageOverEvent += page => 
        {
            SubtitleWriter.Instance.TurnTheNextPage();

            if (page == 2)
            {
                StartCoroutine(SceneLoad(1.5f));
            }
        };
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_Index < _Pages.Length)
            {
                _Image.sprite = _Pages[_Index++];
            }
        }
        if (Input.GetMouseButtonDown(2))
        {
            SubtitleWriter.Instance.WriteSubtitle();
        }
    }

    private IEnumerator SceneLoad(float wait)
    {
        _AlphaFader.AlphaFade(0f, wait);
        yield return new WaitForSeconds(wait);

        _MetalicBG.AlphaFadeRegular(1f, wait/2);
        yield return new WaitForSeconds(wait/2);

        SceneManager.LoadScene(IntroScene.MainSceneBuildIndex);
    }
}

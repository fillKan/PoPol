using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Presenter : MonoBehaviour
{
    [Header("Audio Section")]
    [SerializeField] private Audio _BackgroundMusic;
    [SerializeField] private float _Volume;

    [Space()]
    [SerializeField] private AlphaFader _MetalicBG;
    [SerializeField] private AlphaFader _AlphaFader;
    [SerializeField] private Image _Image;

    [Header("Presentation Sprite")]
    [SerializeField] private Sprite[] _Pages;

    [SerializeField] private bool Last;
    [SerializeField] private GameObject Li;
    private int _Index;

    private void Awake()
    {
        _Index = 1;

        _BackgroundMusic?.VoulmeCotrol(1.5f, _Volume);
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
            if (Last)
            {
                Li.SetActive(true);
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
        _BackgroundMusic?.VoulmeCotrol(wait * 1.3f, 0f);

        _AlphaFader.AlphaFade(0f, wait);
        yield return new WaitForSeconds(wait);

        _MetalicBG.AlphaFadeRegular(1f, wait/2);
        yield return new WaitForSeconds(wait/2);

        SceneManager.LoadScene(IntroScene.MainSceneBuildIndex);
    }
}

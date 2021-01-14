using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class IntroScene : MonoBehaviour
{
    [Header("Intro Direction")]
    [SerializeField] private VideoPlayer _Video;
    [SerializeField] private AlphaFader _AlphaFader;

    [Header("Orientation Object")]
    [SerializeField] private GameObject _Orientation;
    [SerializeField] private Animator _Animator;

    private void Awake()
    {
        MainCamera.Instance.SetColor(Color.black);
    }
    private void Start()
    {
         _AlphaFader.SetAlpha(0f);
        _Orientation.SetActive(false);

        _Video.loopPointReached += VideoPlayCompleted;

        SubtitleWriter.Instance.PageOverEvent += i => 
        {
            if (i == 0)
            {
                int hash = _Animator.GetParameter(0).nameHash;

                _Animator.SetBool(hash, true);
            }
        };
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SubtitleWriter.Instance.TurnThePrevPage();
        }
        if (Input.GetMouseButtonDown(1))
        {
            SubtitleWriter.Instance.TurnTheNextPage();
        }
        if (Input.GetMouseButtonDown(2))
        {
            SubtitleWriter.Instance.WriteSubtitle();
        }
    }
    private void VideoPlayCompleted(VideoPlayer source)
    {
        _Video.targetCameraAlpha = 0f;
        StartCoroutine(Direction());
    }
    private IEnumerator Direction()
    {
        float videFadeTime = 1.5f;
        for (float i = 0f; i < videFadeTime; i += Time.deltaTime)
        {
            float ratio = Mathf.Min(i / videFadeTime, 1f);

            _Video.targetCameraAlpha = Mathf.Lerp(1f, 0f, ratio);
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        _AlphaFader.AlphaFadeRegular(1f, 2f);

        yield return new WaitForSeconds(2.5f);
        _AlphaFader.AlphaFadeRegular(0f, 1f);

        yield return new WaitForSeconds(3.0f);
        _Orientation.SetActive(true);
    }
}

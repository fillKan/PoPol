using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class IntroScene : MonoBehaviour
{
    [SerializeField] private VideoPlayer _Video;

    [Header("AlphaFader")]
    [SerializeField] private AlphaFader _AlphaFader;
    [SerializeField] private AlphaFader _MadeByFader;

    private void Awake()
    {
        MainCamera.Instance.SetColor(Color.black);
    }
    private void Start()
    {
         _AlphaFader.SetAlpha(0f);
        _MadeByFader.SetAlpha(0f);

        _Video.loopPointReached += VideoPlayCompleted;
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
        _AlphaFader.AlphaFadeRegular(1f, 3f);

        yield return new WaitForSeconds(3.5f);
        _AlphaFader.AlphaFadeRegular(0f, 2f);

        yield return new WaitForSeconds(2.5f);
        _MadeByFader.AlphaFadeRegular(1f, 3f);

        yield return new WaitForSeconds(3.5f);
        _MadeByFader.AlphaFadeRegular(0f, 2f);
    }
}

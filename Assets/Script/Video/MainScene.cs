using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScene : MonoBehaviour
{
    [SerializeField] private Animator _Animator;
    [SerializeField] private AlphaFader _AlphaFader;

    private ContentBlock[] _Sections;

    private void Awake()
    {
        _Sections = FindObjectsOfType<ContentBlock>();

        for (int i = 0; i < _Sections.Length; i++)
        {
            _Sections[i].SelectedClickEvent += section => 
            {
                StartCoroutine(Enumerator(section));
            };
        }
    }
    private IEnumerator Enumerator(ContentBlock content)
    {
        if (content.TryGetComponent(out RectTransform contentRect))
        {
            contentRect.parent = contentRect.parent.parent;

            Vector2 targetPosition = contentRect.anchoredPosition;
                    targetPosition.y += -30f;
            Vector2 refVelocity = Vector2.zero;

            for (float i = 0f; i < 0.5f; i += Time.deltaTime)
            {
                contentRect.anchoredPosition =
                    Vector2.SmoothDamp(contentRect.anchoredPosition, targetPosition, ref refVelocity, 0.3f);

                yield return null;
            }
            bool alreadyPlayAnim = false;

            _AlphaFader.SetAlpha(0f);

            for (float i = 0f; i < 2.5f; i += Time.deltaTime)
            {
                float ratio = Mathf.Min(i / 1f, 1f);

                if (ratio > 0.8f && !alreadyPlayAnim)
                {
                    alreadyPlayAnim = true;
                    content.PlayAnimation(ContentBlock.AnimationType.Close);
                }
                contentRect.anchoredPosition =
                    Vector2.Lerp(contentRect.anchoredPosition, new Vector2(contentRect.anchoredPosition.x, +400f), ratio);

                yield return null;
            }
            SceneManager.LoadScene(content.AttachSceneIndex);
        }
    }
    public void AnimatorDisable()
    {
        _Animator.enabled = false;
    }
}

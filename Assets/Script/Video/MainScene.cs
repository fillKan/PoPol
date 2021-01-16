using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScene : MonoBehaviour
{
    [SerializeField] private Animator _Animator;
    [SerializeField] private AlphaFader _AlphaFader;

    private void Awake()
    {
        var sections = FindObjectsOfType<ContentBlock>();

        for (int i = 0; i < sections.Length; i++)
        {
            sections[i].SelectedClickEvent += section => 
            {
                // MainScene은 섹션 블럭들을 포함하는 캔버스이기 때문에 가능한 코드
                section.transform.parent = transform;
                section.PlayAnimation(ContentBlock.AnimationType.Default);
                
                StartCoroutine(Enumerator(section));
            };
        }
    }
    private IEnumerator Enumerator(ContentBlock content)
    {
        content.TryGetComponent(out RectTransform contentRect);

        yield return new WaitForSeconds(ContentBlock.AnimationTime);
        _AlphaFader.AlphaFade (0f, ContentBlock.AnimationTime);

        Vector2 center = new Vector2(960.0f, -540.0f);
        for (float i = 0f; i < 1f; i += Time.deltaTime)
        {
            float ratio = Mathf.Min(i / 1f, 1f);

            contentRect.anchoredPosition =
                Vector2.Lerp(contentRect.anchoredPosition, center, ratio);

            yield return null;
        }
        contentRect.anchoredPosition = center;
    }

    public void AnimatorDisable()
    {
        _Animator.enabled = false;
    }
}

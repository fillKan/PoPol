using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContentBlock : MonoBehaviour, IPointerDownHandler
{
    public enum AnimationType
    {
        Open, Default, Close
    }

    public const float AnimationTime = 1f;

    public int AttachSceneIndex
    {
        get => _AttachSceneIndex;
    }
    public Color BackGroundColor
    { get => _BackgroundColor; }

    public event Action<ContentBlock> SelectedClickEvent;

    [SerializeField] private int _AttachSceneIndex;
    [SerializeField] private ContentBlockController _Controller;
    
    [Header("AlphaFader")]
    [SerializeField] private AlphaFader _ThisAlphaFader;
    [SerializeField] private AlphaFader _SideAlphaFader;

    [Header("Animation")]
    [SerializeField] private Vector2 _TranslatePosition;
    [SerializeField] private Color _BackgroundColor;

    private bool _IsOpend;
    private Coroutine _AnimRoutine;

    private void Awake()
    {
        _AnimRoutine = new Coroutine(this);
    }
    private void Start()
    {
        _SideAlphaFader.SetAlpha(0f);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (Input.GetMouseButtonDown(0))
        {
            _Controller.BlockSelect(this, _IsOpend);
            _Controller.Transition(_TranslatePosition, AnimationTime);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            if (_IsOpend)
            {
                SelectedClickEvent?.Invoke(this);
            }
        }
    }
    public void PlayAnimation(AnimationType type)
    {
        float alpha;
        float scale;

        switch (type)
        {
            case AnimationType.Open:
                alpha = 1f;
                scale = 1f;
                break;

            case AnimationType.Default:
                alpha = 1f;
                scale = 0.4f;
                break;

            case AnimationType.Close:
                alpha = 0.05f;
                scale = 0.4f;
                break;

            default:
                alpha = 1f;
                scale = 1f;
                break;
        }
        _IsOpend = type == AnimationType.Open;
        _AnimRoutine.StartRoutine(Animation(alpha, scale));
    }

    private IEnumerator Animation(float alpha, float scale)
    {
        yield return null;
        _ThisAlphaFader.AlphaFade(alpha, AnimationTime);

        if (_IsOpend)
        {
            _SideAlphaFader.AlphaFade(1f, AnimationTime);
        }
        else
        {
            _SideAlphaFader.AlphaFade(0f, AnimationTime);
        }
        Vector2 targetScale = Vector2.one * scale;

        for (float i = 0f; i < AnimationTime; i += Time.deltaTime)
        {
            float ratio = Mathf.Min(AnimationTime, i) / AnimationTime;
            transform.localScale = Vector2.Lerp(transform.localScale, targetScale, ratio);

            _Controller.LayoutRebuild();
            yield return null;
        }
        _AnimRoutine.FinshRoutine();
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ContentBlockController : MonoBehaviour
{
    [SerializeField] private RectTransform _RectTransform;
    [SerializeField] private ContentBlock[] _ContentBlocks;

    private Coroutine _Transition;

    private void Awake()
    {
        _Transition = new Coroutine(this);
    }
    public void BlockSelect(ContentBlock block, bool isOpend)
    {
        ContentBlock.AnimationType animationType;

        if (isOpend)
        {
            animationType = ContentBlock.AnimationType.Default;
        }
        else
        {
            animationType = ContentBlock.AnimationType.Close;
        }
        for (int i = 0; i < _ContentBlocks.Length; ++i)
        {
            _ContentBlocks[i].PlayAnimation(animationType);
        }
        if (isOpend)
        {
            MainCamera.Instance.ColorChange(ContentBlock.AnimationTime, Color.white);
        }
        else
        {
            MainCamera.Instance.ColorChange(2.5f, block.BackGroundColor);

            block.PlayAnimation(ContentBlock.AnimationType.Open);
        }
    }
    public void Transition(Vector2 transition, float time)
    {
        _Transition.StartRoutine(ETransition(transition, time));
    }
    public void LayoutRebuild()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(_RectTransform);
    }
    private IEnumerator ETransition(Vector2 targetPos, float time)
    {
        for (float i = 0f; i < time; i += Time.deltaTime)
        {
            float ratio = Mathf.Min(time, i) / time;

            transform.localPosition = Vector2.Lerp(transform.localPosition, targetPos, ratio);
            yield return null;
        }
        _Transition.FinshRoutine();
    }
}

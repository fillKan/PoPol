using UnityEngine;

public class SinCurve : MonoBehaviour
{
    [Header("Curve Info")]
    [Range(0.1f, 2f)]
    [SerializeField] private float _CurveScale;
    [SerializeField] private float _Period;
    [SerializeField] private float _ScaleMultiply;

    [Header("Other Info")]
    [SerializeField] private SpriteRenderer Renderer;

    private Vector3 _OriginPosition;
    private float _SumTime;


    public void SetSprite(Sprite sprite)
    {
        Renderer.sprite = sprite;
    }
    public void SetSprite(Sprite sprite, Color color)
    {
        Renderer.color = color;
        Renderer.sprite = sprite;
    }

    private void OnEnable()
    {
        _SumTime = 0f;

        _OriginPosition = transform.localPosition;
        Update();
    }

    private void Update()
    {
        _SumTime += Time.deltaTime;

        float sin = Mathf.Sin(Mathf.PI * _SumTime * _Period) * _CurveScale;
        float cos = Mathf.Cos(Mathf.PI * _SumTime * _Period) * _CurveScale;

        transform.localPosition = _OriginPosition + Vector3.up * sin + Vector3.right * cos;

        transform.localScale = Vector3.one * sin * _ScaleMultiply;
    }
}

using UnityEngine;

public class SinCurve : MonoBehaviour
{
    [Range(0.1f, 2f)]
    [SerializeField] private float _CurveScale;
    [SerializeField] private float _Period;
    [SerializeField] private float _ScaleMultiply;

    private Vector3 _OriginPosition;
    private Vector3 _OriginScale;

    private void Start()
    {
        _OriginPosition = transform.localPosition;
        _OriginScale = transform.localScale;
    }

    private void Update()
    {
        float sin = Mathf.Sin(Mathf.PI * Time.time * _Period) * _CurveScale;
        float cos = Mathf.Cos(Mathf.PI * Time.time * _Period) * _CurveScale;

        transform.localPosition = _OriginPosition + Vector3.up * sin + Vector3.right * cos;

        transform.localScale = (Vector3.one * sin * _ScaleMultiply); // + (Vector3.right * cos * _ScaleMultiply);
    }
}

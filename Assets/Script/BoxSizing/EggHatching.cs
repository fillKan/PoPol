using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggHatching : MonoBehaviour
{
    [Header("Sprite Section")]
    [SerializeField] private SpriteRenderer _Renderer;

    [SerializeField] private Sprite _EggSprite;
    [SerializeField] private Sprite _HatchingSprite;
    [SerializeField] private Sprite _ChickSprite;
    [SerializeField] private Sprite  _DinoSprite;

    [Header("Scaling Info")]
    [SerializeField] private float _CursorRadius;
    [SerializeField] private float _MinScale;
    [SerializeField] private float _MaxScale;

    private void Update()
    {
        Vector2 CursorPoint()
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        float distance
            = Mathf.Max(Vector2.Distance(transform.position, CursorPoint()) - _CursorRadius, 0f);

        transform.localScale
            = Vector2.one * Mathf.Max(_MaxScale - distance, _MinScale);

        float ratio = Mathf.InverseLerp(_MaxScale, _MinScale, transform.localScale.x);

        if (ratio > 0.75f)
        {
            _Renderer.sprite = _EggSprite;
        }
        else if (ratio > 0.3f)
        {
            _Renderer.sprite = _HatchingSprite;
        }
        else if (!_Renderer.sprite.Equals(_DinoSprite) && !_Renderer.sprite.Equals(_ChickSprite))
        {
            if (Random.value < 0.05f)
            {
                _Renderer.sprite = _DinoSprite;
            }
            else 
                _Renderer.sprite = _ChickSprite;

            MainCamera.Instance.Shake(0.08f, 0.2f);
        }
        _Renderer.color = new Color(1, 1, 1, Mathf.Max(_MaxScale - distance, 0.15f));
    }
}

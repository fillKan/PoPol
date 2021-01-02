using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinCurveGenerator : MonoBehaviour
{
    [SerializeField] private SinCurve _SinCurve;
    [SerializeField] private Sprite[] _Sprites;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var sprite = _Sprites[Random.Range(0, _Sprites.Length)];

            Instantiate(_SinCurve, point, Quaternion.identity).SetSprite(sprite);
        }
    }
}

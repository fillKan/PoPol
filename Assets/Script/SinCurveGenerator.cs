using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinCurveGenerator : MonoBehaviour
{
    [SerializeField] private SinCurve _SinCurve;
    [SerializeField] private Sprite[] _Sprites;

    [Header("Special Fish")]
    [Range(0.0f,1f)]
    [SerializeField] private float _Probability;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            var sprite = _Sprites[Random.Range(0, _Sprites.Length)];
            var sinCur = Instantiate(_SinCurve, point, Quaternion.identity);

            if (Random.value <= _Probability)
            {
                sinCur.SetSprite(sprite, Color.yellow);
            }
            else
            {
                sinCur.SetSprite(sprite);
            }
        }
    }
}

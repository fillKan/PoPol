using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PolarCoordControlar : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] private float _Probablity;
    [SerializeField] private PolarCoordMovement _PolarCoordMovement;
    [SerializeField] private PolarCoordMovement _Special;

    private List<PolarCoordMovement> _PolarCoords;
    private float _StartCameraScale;

    private void Awake()
    {
        _StartCameraScale = Camera.main.orthographicSize;

        _PolarCoords = new List<PolarCoordMovement>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PolarCoordMovement polarCoord;

            if (Random.value <= _Probablity)
            {
                polarCoord = Instantiate(_Special, Vector2.zero, Quaternion.identity);
            }
            else 
                polarCoord = Instantiate(_PolarCoordMovement, Vector2.zero, Quaternion.identity);

             polarCoord.Update();
            _PolarCoords.Add(polarCoord);
        }
        if (_PolarCoords.Count > 0)
        {
            _PolarCoords.ForEach(o =>
            {
                float v = Input.GetAxis("Vertical")   * Time.deltaTime * 2f;
                float h = Input.GetAxis("Horizontal") * Time.deltaTime * 2f;

                o.AdditionMovement(v, h);
            });
        }
    }
}

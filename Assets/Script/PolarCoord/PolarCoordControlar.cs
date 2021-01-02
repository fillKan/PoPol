using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PolarCoordControlar : MonoBehaviour
{
    [SerializeField] private PolarCoordMovement _PolarCoordMovement;

    private List<PolarCoordMovement> _PolarCoords;

    private void Awake()
    {
        _PolarCoords = _PolarCoords ?? new List<PolarCoordMovement>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var polarCoord = Instantiate(_PolarCoordMovement, Vector3.zero, Quaternion.identity);

             polarCoord.Update();
            _PolarCoords.Add(polarCoord);
        }
    }
}

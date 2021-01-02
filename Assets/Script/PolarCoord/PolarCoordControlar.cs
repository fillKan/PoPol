using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PolarCoordControlar : MonoBehaviour
{
    [SerializeField] private PolarCoordMovement _PolarCoordMovement;

    private List<PolarCoordMovement> _PolarCoords;

    private void Awake()
    {
        _PolarCoords = new List<PolarCoordMovement>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var polarCoord = Instantiate(_PolarCoordMovement, Vector3.zero, Quaternion.identity);

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public enum Coordinate { World, Local }

public class PolarCoordMovement : MonoBehaviour
{
    [SerializeField] private Coordinate _Coordinate;
    [SerializeField] private float _Speed;
    [SerializeField] private float _Radius;

    private float _Theta;

    public void AdditionMovement(float speed, float radius)
    {
        _Speed += speed;
        _Radius += radius;
    }

    public void Update()
    {
        transform.PolarCoord(_Radius, _Theta, _Coordinate);

        if (_Radius != 0)
        {
            _Theta += Time.deltaTime / _Radius * _Speed;
        }
    }
}

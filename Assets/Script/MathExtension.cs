using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathExtension
{
    public static void PolarCoord(this Transform transform, float radius, float theta, Coordinate coordinate = Coordinate.World)
    {
        Vector2 coord = new Vector2(Mathf.Sin(theta) * radius, Mathf.Cos(theta) * radius);

        switch (coordinate)
        {
            case Coordinate.World:
                transform.position = coord;
                break;

            case Coordinate.Local:
                transform.localPosition = coord;
                break;
        }
    }
}

using UnityEngine;

public class BezierCurve : MonoBehaviour
{
    public Transform[] controlPoints; // Array of control points

    public Vector3 GetPoint(float t)
    {
        if (controlPoints.Length < 2)
        {
            Debug.LogError("At least 2 control points are needed for a Bezier curve.");
            return Vector3.zero;
        }
        return CalculateBezierPoint(t, controlPoints);
    }

    private Vector3 CalculateBezierPoint(float t, Transform[] points)
    {
        // Implementing De Casteljau's algorithm
        int n = points.Length - 1;
        Vector3[] tempPoints = new Vector3[points.Length];

        for (int i = 0; i < points.Length; i++)
        {
            tempPoints[i] = points[i].position;
        }

        for (int r = 1; r <= n; r++)
        {
            for (int i = 0; i <= n - r; i++)
            {
                tempPoints[i] = (1 - t) * tempPoints[i] + t * tempPoints[i + 1];
            }
        }

        return tempPoints[0];
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class CMath
{
	static float RAD_TO_DEg = 180 / Mathf.PI;
	static float DEG_TO_RAD = Mathf.PI / 180;
    /// <summary>
    /// A Random int between the given numbers, both inclusive.
    /// </summary>
    /// <param name="aMin"></param>
    /// <param name="aMax"></param>
    /// <returns></returns>
	public static int randomIntBetween(int aMin, int aMax)
	{
		return Random.Range (aMin, aMax + 1);
	}

    /// <summary>
    /// A Random float between the given numbers, both inclusive.
    /// </summary>
    /// <param name="aMin"></param>
    /// <param name="aMax"></param>
    /// <returns></returns>
	public static float RandomFloatBetween(float aMin, float aMax)
	{
		return Random.Range (aMin, aMax);
	}

    public static float NormalBetweenValues(float min, float max, float v)
    {
        return (v - min) / (max - min);
    }

    public static Vector3 NormalBetweenVector3(Vector3 min, Vector3 max, Vector3 v)
    {
        //return DivideVector3((v - min), (max - min)).magnitude;
        return new Vector3(
            NormalBetweenValues(min.x, max.x, v.x),
            NormalBetweenValues(min.y, max.y, v.y),
            NormalBetweenValues(min.z, max.z, v.z));
    }

    public static float clampDeg(float aDeg)
	{
		aDeg = aDeg % 360.0f;
		if (aDeg < 0.0f) 
		{
			aDeg += 360.0f;
		}

		return aDeg;
	}

    public static Vector3 InsideUnitCircleXZ()
    {
        Vector3 vector = Random.insideUnitCircle;
        return new Vector3(vector.x, 0f, vector.y);
    }

    public static Vector3 CubeBezier3(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        float r = 1f - t;
        float f0 = r * r * r;
        float f1 = r * r * t * 3;
        float f2 = r * t * t * 3;
        float f3 = t * t * t;
        return f0 * p0 + f1 * p1 + f2 * p2 + f3 * p3;
    }

    /// <summary>
    /// Determines if a Point is in a rectangle. Rect with pivot on top-left.
    /// </summary>
    /// <param name="aX"></param>
    /// <param name="aY"></param>
    /// <param name="aRectX"></param>
    /// <param name="aRectY"></param>
    /// <param name="aRectWidth"></param>
    /// <param name="aRectHeight"></param>
    /// <returns></returns>
	public static bool pointInRect(float aX, float aY, float aRectX, float aRectY, float aRectWidth, float aRectHeight)
	{
		return (aX >= aRectX && aX <= aRectX + aRectWidth && aY <= aRectY && aY >= aRectY - aRectHeight);
	}

    public static bool RectInRectCenterXZ(Vector3 pos1, Vector2 size1, Vector3 pos2, Vector2 size2)
    {
        return pos1.x - size1.x / 2 < pos2.x + size2.x / 2 &&
            pos1.x + size1.x / 2 > pos2.x - size2.x / 2 &&
            pos1.z + size1.y / 2 > pos2.z - size2.y / 2 &&
            pos1.z - size1.y / 2 < pos2.z + size2.y / 2;
    }

public static float dist(float aX1, float aY1, float aX2, float aY2)
	{
		return Mathf.Sqrt((aX2 - aX1) * (aX2 - aX1) + (aY2 - aY1) * (aY2 - aY1));
	}

	public static float distSq(float aX1, float aY1, float aX2, float aY2)
	{
		return (aX2 - aX1) * (aX2 - aX1) + (aY2 - aY1) * (aY2 - aY1);
	}

	public static float min(float aValue1, float aValue2)
	{
		if (aValue1 < aValue2)
		{
			return aValue1;
		}
		
		return aValue2;
	}
	
	// Convert from radians to degrees.
	public static float radToDeg(float aAngle)
	{
		return aAngle * RAD_TO_DEg;
	}
	
	// Convert from degrees to radians.
	public static float degToRad(float aAngle)
	{
		return aAngle * DEG_TO_RAD;
	}

    public static float Sin(float aDeg)
    {
        return Mathf.Sin(aDeg * DEG_TO_RAD);
    }

    public static float Cos(float aDeg)
    {
        return Mathf.Cos(aDeg * DEG_TO_RAD);
    }

    public static Vector3 SetAngleDeg(this Vector3 vector, float aAngle)
    {
        float lenght = vector.magnitude;
        vector.x = Mathf.Cos(degToRad(aAngle)) * lenght;
        vector.z = Mathf.Sin(degToRad(aAngle)) * lenght;
        return vector;
    }

    public static Vector3 SetAngle(this Vector3 vector, float aAngle)
    {
        float lenght = vector.magnitude;
        vector.x = Mathf.Cos(aAngle) * lenght;
        vector.z = Mathf.Sin(aAngle) * lenght;
        return vector;
    }

    static public void DrawGUILabel(Vector3 pos, string text)
    {
        var point = Camera.main.WorldToScreenPoint(pos);
        GUI.Label(new Rect(point.x, Screen.height - point.y, 200, 200), text);
    }

    static public void DrawGUILabel(Vector3 pos, string text, Color color)
    {
        var point = Camera.main.WorldToScreenPoint(pos);
        GUIStyle style = new GUIStyle();
        style.normal.textColor = color;
        GUI.Label(new Rect(point.x, Screen.height - point.y, 200, 200), text, style);
    }

    public static List<Vector2> OrderByDistance(List<Vector2> pointList, Vector2 point)
    {
        List<Vector2> sub = new List<Vector2>();
        pointList.ForEach(item => sub.Add(new Vector2(item.x - point.x, item.y - point.y)));

        sub.Sort((a, b) =>
        {
            double d2 = Mathf.Pow(b.x, 2) + Mathf.Pow(b.y, 2);
            double d1 = Mathf.Pow(a.x, 2) + Mathf.Pow(a.y, 2);
            return d1.CompareTo(d2);
        });

        List<Vector2> sorted = new List<Vector2>();
        sub.ForEach(item => sorted.Add(new Vector2(item.x + point.x, item.y + point.y)));


        return sorted;
    }

    private static double Distance(Vector2 p1, Vector2 p2)
    {
        return Mathf.Sqrt(Mathf.Pow(p2.x - p1.x, 2) + Mathf.Pow(p2.y - p1.y, 2));
    }
    private static double DistanceQuick(Vector2 p1, Vector2 p2)
    {
        // distance will this or less
        double deltaX = Mathf.Abs(p2.x - p1.x);
        double deltaY = Mathf.Abs(p2.y - p1.y);
        return deltaX > deltaY ? deltaX : deltaY;
    }

    public static float Clamp(float min, float max, float aValue)
    {
        if (aValue < min)
        {
            return min;
        }
        if (aValue > max)
        {
            return max;
        }
        return aValue;
    }

    /// <summary>
    /// Determines if a Point is in a rectangle. Rect with pivot on top-left.
    /// </summary>
    /// <param name="aX"></param>
    /// <param name="aY"></param>
    /// <param name="aRectX"></param>
    /// <param name="aRectY"></param>
    /// <param name="aRectWidth"></param>
    /// <param name="aRectHeight"></param>
    /// <returns></returns>
    public static bool pointInRect2D(float aX, float aY, float aRectX, float aRectY, float aRectWidth, float aRectHeight)
    {
        return (aX >= aRectX && aX <= aRectX + aRectWidth && aY >= aRectY && aY <= aRectY + aRectHeight);
    }
}

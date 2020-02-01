using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public static class CExtensions
{

    static public RectTransform rectTransform(this MonoBehaviour self)
    {
        return self.transform as RectTransform;
    }

    public static Vector3 X0Y(this Vector2 v)
    {
        return new Vector3(v.x, 0, v.y);
    }

    public static Vector3 XY0(this Vector2 v)
    {
        return new Vector3(v.x, v.y,0 );
    }

    public static Vector3 XY0(this Vector3 v)
    {
        return new Vector3(v.x, v.y, 0);
    }

    public static Vector3 X1Y(this Vector2 v)
    {
        return new Vector3(v.x, 1, v.y);
    }

    public static Vector2 XZ(this Vector3 v)
    {
        return new Vector2(v.x, v.z);
    }

    public static Vector2 XY(this Vector3 v)
    {
        return new Vector2(v.x, v.y);
    }

    public static Vector3 X0Z(this Vector3 v)
    {
        return new Vector3(v.x, 0, v.z);
    }

    public static Vector3 X0Y(this Vector3 v)
    {
        return new Vector3(v.x, 0, v.y);
    }

    public static Vector3 XZ0(this Vector3 v)
    {
        return new Vector3(v.x, v.z, 0);
    }

    public static Color WithAlpha(this Color c, float alpha)
    {
        return new Color(c.r, c.g, c.b, alpha);
    }

    /// <summary>
    /// Returns the angle in degrees.
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static float AngleXZ(this Vector3 v)
    {
        return Mathf.Atan2(v.z, v.x) * Mathf.Rad2Deg;
    }

    public static void DebugPosition(this Vector3 pos, float size, Color col, float duration = 10)
    {
        Debug.DrawLine(pos + Vector3.forward * size / 2, pos - Vector3.forward * size / 2, col, duration);
        Debug.DrawLine(pos + Vector3.left * size / 2, pos + Vector3.right * size / 2, col, duration);
        Debug.DrawLine(pos + Vector3.up * size / 2, pos - Vector3.up * size / 2, col, duration);
    }

    /// <summary>
    /// Draws a rectangle with it's centre on the given position.
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="size"></param>
    public static void DebugRectangleXZ(Vector3 pos, Vector2 size, Color color, float duration = 10)
    {
        Debug.DrawLine(pos - Vector3.right * size.x / 2 + Vector3.forward * size.y / 2, pos + Vector3.right * size.x / 2 + Vector3.forward * size.y / 2, color, duration);
        Debug.DrawLine(pos - Vector3.right * size.x / 2 - Vector3.forward * size.y / 2, pos + Vector3.right * size.x / 2 - Vector3.forward * size.y / 2, color, duration);
        Debug.DrawLine(pos - Vector3.right * size.x / 2 + Vector3.forward * size.y / 2, pos - Vector3.right * size.x / 2 - Vector3.forward * size.y / 2, color, duration);
        Debug.DrawLine(pos + Vector3.right * size.x / 2 + Vector3.forward * size.y / 2, pos + Vector3.right * size.x / 2 - Vector3.forward * size.y / 2, color, duration);
    }

    public static void Clamp(this Vector3 v, float max)
    {
        if (v.sqrMagnitude > max * max)
        {
            v.Normalize();
            v = v * max;
        }
    }

    public static bool IsTransparent(this Texture2D tex)
    {
        for (int x = 0; x < tex.width; x++)
            for (int y = 0; y < tex.height; y++)
                if (tex.GetPixel(x, y).a != 0)
                    return false;
        return true;
    }

    public static bool IsPointInRectangleXZ(Vector2 pos, Vector2 recLowerLeftCorner, float height, float width)
    {
        bool inarea = pos.x > recLowerLeftCorner.x &&
            pos.x < recLowerLeftCorner.x + width &&
            pos.y > recLowerLeftCorner.y &&
            pos.y < recLowerLeftCorner.y + height;
        //DebugRectangleXZ(recLowerLeftCorner.X0Y() + new Vector3(width / 2, 0, height / 2), new Vector2(width, height), Color.yellow);
        //DebugPosition(pos.X0Y(), 2, inarea ? Color.green : Color.red);
        return inarea;
    }

}
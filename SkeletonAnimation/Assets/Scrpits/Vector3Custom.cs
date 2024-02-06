// Vector3Custom.cs
using UnityEngine;

public class Vector3Custom
{
    public float x, y, z;

    public Vector3Custom(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public static Vector3Custom operator +(Vector3Custom v1, Vector3Custom v2)
    {
        return new Vector3Custom(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
    }

    public static Vector3Custom operator -(Vector3Custom v1, Vector3Custom v2)
    {
        return new Vector3Custom(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
    }

    public static Vector3Custom operator *(float scalar, Vector3Custom v)
    {
        return new Vector3Custom(scalar * v.x, scalar * v.y, scalar * v.z);
    }

    public float Magnitude()
    {
        return Mathf.Sqrt(x * x + y * y + z * z);
    }

    public static Vector3Custom Normalize(Vector3Custom v)
    {
        float length = Mathf.Sqrt(v.x * v.x + v.y * v.y + v.z * v.z);

        if (length > 0)
        {
            return new Vector3Custom(v.x / length, v.y / length, v.z / length);
        }
        else
        {
            return new Vector3Custom(0, 0, 0); // Handle division by zero
        }
    }

    public Vector3 ToUnityVector()
    {
        return new Vector3(x, y, z);
    }
}

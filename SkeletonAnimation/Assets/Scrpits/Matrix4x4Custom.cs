// Matrix4x4Custom.cs
using UnityEngine;

public class Matrix4x4Custom
{
    private float[,] matrix;

    public Matrix4x4Custom()
    {
        matrix = new float[4, 4];
    }

    public float this[int row, int col]
    {
        get { return matrix[row, col]; }
        set { matrix[row, col] = value; }
    }

    public static Matrix4x4Custom operator *(Matrix4x4Custom m1, Matrix4x4Custom m2)
    {
        Matrix4x4Custom result = new Matrix4x4Custom();

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                result[i, j] = 0;
                for (int k = 0; k < 4; k++)
                {
                    result[i, j] += m1[i, k] * m2[k, j];
                }
            }
        }

        return result;
    }
    public static Matrix4x4Custom Identity()
    {
        Matrix4x4Custom identityMatrix = new Matrix4x4Custom();
        for (int i = 0; i < 4; i++)
        {
            identityMatrix[i, i] = 1;
        }
        return identityMatrix;
    }

    public static Matrix4x4Custom RotateAxis(float angle, Vector3Custom axis)
    {
        Matrix4x4Custom rotationMatrix = new Matrix4x4Custom();
        float cosAngle = Mathf.Cos(angle);
        float sinAngle = Mathf.Sin(angle);
        axis = Vector3Custom.Normalize(axis);

        rotationMatrix[0, 0] = cosAngle + (1 - cosAngle) * axis.x * axis.x;
        rotationMatrix[0, 1] = (1 - cosAngle) * axis.x * axis.y - sinAngle * axis.z;
        rotationMatrix[0, 2] = (1 - cosAngle) * axis.x * axis.z + sinAngle * axis.y;

        rotationMatrix[1, 0] = (1 - cosAngle) * axis.x * axis.y + sinAngle * axis.z;
        rotationMatrix[1, 1] = cosAngle + (1 - cosAngle) * axis.y * axis.y;
        rotationMatrix[1, 2] = (1 - cosAngle) * axis.y * axis.z - sinAngle * axis.x;

        rotationMatrix[2, 0] = (1 - cosAngle) * axis.x * axis.z - sinAngle * axis.y;
        rotationMatrix[2, 1] = (1 - cosAngle) * axis.y * axis.z + sinAngle * axis.x;
        rotationMatrix[2, 2] = cosAngle + (1 - cosAngle) * axis.z * axis.z;

        rotationMatrix[3, 3] = 1;

        return rotationMatrix;
    }

    public Quaternion ToUnityQuaternion()
    {
        Quaternion quaternion = new Quaternion();

        float trace = matrix[0, 0] + matrix[1, 1] + matrix[2, 2];

        if (trace > 0)
        {
            float s = 0.5f / Mathf.Sqrt(trace + 1.0f);
            quaternion.w = 0.25f / s;
            quaternion.x = (matrix[2, 1] - matrix[1, 2]) * s;
            quaternion.y = (matrix[0, 2] - matrix[2, 0]) * s;
            quaternion.z = (matrix[1, 0] - matrix[0, 1]) * s;
        }
        else
        {
            if (matrix[0, 0] > matrix[1, 1] && matrix[0, 0] > matrix[2, 2])
            {
                float s = 2.0f * Mathf.Sqrt(1.0f + matrix[0, 0] - matrix[1, 1] - matrix[2, 2]);
                quaternion.w = (matrix[2, 1] - matrix[1, 2]) / s;
                quaternion.x = 0.25f * s;
                quaternion.y = (matrix[0, 1] + matrix[1, 0]) / s;
                quaternion.z = (matrix[0, 2] + matrix[2, 0]) / s;
            }
            else if (matrix[1, 1] > matrix[2, 2])
            {
                float s = 2.0f * Mathf.Sqrt(1.0f + matrix[1, 1] - matrix[0, 0] - matrix[2, 2]);
                quaternion.w = (matrix[0, 2] - matrix[2, 0]) / s;
                quaternion.x = (matrix[0, 1] + matrix[1, 0]) / s;
                quaternion.y = 0.25f * s;
                quaternion.z = (matrix[1, 2] + matrix[2, 1]) / s;
            }
            else
            {
                float s = 2.0f * Mathf.Sqrt(1.0f + matrix[2, 2] - matrix[0, 0] - matrix[1, 1]);
                quaternion.w = (matrix[1, 0] - matrix[0, 1]) / s;
                quaternion.x = (matrix[0, 2] + matrix[2, 0]) / s;
                quaternion.y = (matrix[1, 2] + matrix[2, 1]) / s;
                quaternion.z = 0.25f * s;
            }
        }

        return quaternion;
    }
}

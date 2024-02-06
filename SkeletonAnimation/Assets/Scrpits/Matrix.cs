using UnityEngine;


public class Matrix : MonoBehaviour
{
    public Transform targetAxis;
    public Vector3Custom m_vAsix = new Vector3Custom(0, 1, 0);
    public float m_fRatAngle;

    void Start()
    {
        Matrix4x4Custom temp = Matrix4x4Custom.Identity();
        Debug.Log(temp);
    }

    void Update()
    {
        if (targetAxis)
        {
            Vector3Custom vTargetPos = new Vector3Custom(targetAxis.position.x, targetAxis.position.y, targetAxis.position.z);
            Vector3Custom vPos = new Vector3Custom(transform.position.x, transform.position.y, transform.position.z);

            m_vAsix = Vector3Custom.Normalize(vTargetPos - vPos);
        }

        Matrix4x4Custom rotationMatrix = Matrix4x4Custom.RotateAxis(m_fRatAngle * Time.deltaTime, m_vAsix);

        // Apply the rotation matrix to the object's local rotation
        transform.localRotation *= rotationMatrix.ToUnityQuaternion();

        Debug.DrawLine(transform.position, targetAxis.position, Color.red);
    }

    public int idx = 0;

    private void OnGUI()
    {
        int w = 300;
        int h = 100;
        int a = 0;
        GUI.Box(new Rect(w * idx, a * h, w, h),
            gameObject.name + "\n" + transform.localToWorldMatrix.ToString()); a++;
        GUI.Box(new Rect(w * idx, a * h, w, h),
            transform.worldToLocalMatrix.ToString()); a++;
    }
}

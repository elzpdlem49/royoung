using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayWeapon : MonoBehaviour
{
    [SerializeField]
    LayerMask m_sHitLayer;
    [SerializeField]
    float m_fRange = 1;
    [SerializeField]
    Controller m_cController;

    public Controller PlayerController { get => m_cController;}

    public void Initialize(Controller controller, Transform slot)
    {
        m_cController = controller;
        transform.parent = slot;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;
    }

    private void FixedUpdate()
    {
        Vector3 vPos = this.transform.position;
        Vector3 vDir = transform.forward;
        Vector3 vEnd = vPos + vDir * m_fRange;

        RaycastHit raycastHit; 
        bool isCheck = Physics.Raycast(vPos, vDir, out raycastHit, m_fRange, m_sHitLayer);

        if(isCheck)
        {
            Debug.Log("Hit!!");
            //raycastHit.collider.gameObject.GetComponent<SendBack>().Hit(1);
            SendBack sendBack = raycastHit.collider.gameObject.GetComponent<SendBack>();
            sendBack.Hit(m_cController.Atk);
            Debug.DrawLine(vPos, vEnd, Color.green);
        }
        else
        {
            Debug.DrawLine(vPos, vEnd, Color.red);
        }
    }
}

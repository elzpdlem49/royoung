using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //public override void Translate(Vector3 dir, float axis, float speed)
    //{
    //    Vector3 vMove = dir * axis * speed * Time.deltaTime;
    //    m_cAnimationController.Run(axis);
    //    transform.Translate(vMove);
    //}

    //public override void Attack()
    //{
    //    if(m_cEqumentSystem.CheckEqment)
    //        m_cAnimationController.Attack();
    //}

    //public override void EqmentChange()
    //{
    //    if (m_cEqumentSystem.CheckEqment)
    //        m_cEqumentSystem.Release();
    //    else
    //        m_cEqumentSystem.Set(this);
    //}

    // Update is called once per frame
    void Update()
    {
        m_fMove = Input.GetAxis("Vertical");
        m_fRoat = Input.GetAxisRaw("Horizontal");

        Vector3 vMove = Vector3.forward * m_fMove * m_fSpeed * Time.deltaTime;

        float fMoveAbs = Mathf.Abs(m_fMove);

        if (Mathf.Abs(m_fMove) > 0 )
            Translate(Vector3.forward, m_fMove, m_fSpeed);
        else Translate(Vector3.forward, 0, m_fSpeed);

        if (Mathf.Abs(m_fRoat) > 0)
            transform.Rotate(Vector3.up * m_fRoat);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            AttackCancel();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            EqmentChange(); 
        }
    }

    float m_fMove;
    float m_fRoat;

    private void OnGUI()
    {
        GUI.Box(new Rect(0, 0, 100, 20), string.Format("Vertical:{0}", m_fMove));
        GUI.Box(new Rect(0, 20, 100, 20), string.Format("Horizontal:{0}", m_fRoat));
    }
}

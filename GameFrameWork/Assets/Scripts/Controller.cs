using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField]
    AnimationController m_cAnimationController;
    [SerializeField]
    protected float m_fSpeed = 1;
    [SerializeField]
    float m_fAtk = 1;
    [SerializeField]
    EqmentSystem m_cEqumentSystem;

    public float Atk { get => m_fAtk; }

    public virtual void Translate(Vector3 dir, float axis, float speed)
    {
        //Debug.Log(string.Format("Translate!{0}", axis));
        Vector3 vMove = dir * axis * speed * Time.deltaTime;
        m_cAnimationController.Run(axis);
        transform.Translate(vMove);
    }

    public virtual void Attack()
    {
        if (m_cEqumentSystem.CheckEqment)
            m_cAnimationController.Attack();
    }

    public virtual void AttackCancel()
    {
        if (m_cEqumentSystem.CheckEqment)
            m_cAnimationController.AttackCancel();
    }

    public virtual void EqmentChange()
    {
        if (m_cEqumentSystem.CheckEqment)
            m_cEqumentSystem.Release();
        else
            m_cEqumentSystem.Set(this);
    }

    protected void _Initialize()
    {
        m_cAnimationController = GetComponent<AnimationController>();
        m_cEqumentSystem = GetComponent<EqmentSystem>();
    }
}

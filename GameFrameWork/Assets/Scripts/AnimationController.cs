using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class AnimationController : MonoBehaviour
{
    [SerializeField]
    Animator m_cAnimator;

    public void Attack()
    {
        m_cAnimator.SetFloat("Run", 0);
        m_cAnimator.SetTrigger("Attack");
    }

    public void AttackCancel()
    {
        Debug.Log("AttackCancel!");
        m_cAnimator.SetBool("Cancel", true);
    }

    public void Run(float input)
    {
        m_cAnimator.SetFloat("Run", input);
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(string.Format("{0}.{1}.Start()", this.gameObject.name, this));
        m_cAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        //int w = 500;
        //int h = 20;
        //AnimatorClipInfo[] aniClipinfo = m_cAnimator.GetCurrentAnimatorClipInfo(0);
        //AnimatorStateInfo aniStatusInfo = m_cAnimator.GetCurrentAnimatorStateInfo(0);

        //string log = string.Format("{0}:{1}", aniClipinfo[0].clip.name, aniStatusInfo.normalizedTime);
        //GUI.Box(new Rect(0, 0, 500, 20), log);

        //var list = m_cAnimator.parameters;

        //for(int i = 0; i < list.Length; i++)
        //{
        //    string name = list[i].name;
        //    if (GUI.Button(new Rect(0, h * (i + 1),w,h), name))
        //    {
        //        switch(list[i].type)
        //        {
        //            case AnimatorControllerParameterType.Int:
        //                m_cAnimator.SetInteger(i, 0);
        //                break;
        //            case AnimatorControllerParameterType.Float:
        //                m_cAnimator.SetFloat(name, 1);
        //                break;
        //            case AnimatorControllerParameterType.Bool:
        //                m_cAnimator.SetBool(name, true);
        //                break;
        //            case AnimatorControllerParameterType.Trigger:
        //                m_cAnimator.SetTrigger(name);
        //                break;
        //        }
        //    }
        //}
    }
}

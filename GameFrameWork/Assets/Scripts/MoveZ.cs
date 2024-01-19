using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveZ : MonoBehaviour
{
    public Animator m_cAnimator;
    // Start is called before the first frame update
    void Start()
    {
        //Destroy(this.gameObject, 3);
        m_cAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        int w = 500;
        int h = 20;
        AnimatorClipInfo[] aniClipinfo = m_cAnimator.GetCurrentAnimatorClipInfo(0);
        AnimatorStateInfo aniStatusInfo = m_cAnimator.GetCurrentAnimatorStateInfo(0);

        string log = string.Format("{0}:{1}", aniClipinfo[0].clip.name, aniStatusInfo.normalizedTime);
        GUI.Box(new Rect(0, 0, 500, 20), log);

        if(aniStatusInfo.normalizedTime >= 3)
        {
            Destroy(this.gameObject);
        }
    }
}

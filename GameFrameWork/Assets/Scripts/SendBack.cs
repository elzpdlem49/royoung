using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendBack : MonoBehaviour
{
    [SerializeField]
    double m_fHp;
    [SerializeField]
    float m_HitTime;
    bool m_isHit;

    [SerializeField]
    Material m_cMaterial;

    IEnumerator ProccessTimmer(float time)
    {
        m_isHit = true;
        m_cMaterial.color = Color.red;
        yield return new WaitForSeconds(time);
        m_cMaterial.color = Color.white;
        m_isHit = false;
    }

    public void Hit(float demage)
    {
        if (!m_isHit)
        {
            m_fHp -= demage;
            StartCoroutine(ProccessTimmer(m_HitTime));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        int w = 100;
        int h = 20;
        GUI.Box(new Rect(Screen.width - w, 0, w, h), string.Format("HP:{0}",m_fHp));
    }
}

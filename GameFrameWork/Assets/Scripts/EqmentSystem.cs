using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EqmentSystem : MonoBehaviour
{
    [SerializeField]
    List<Transform> listWeapon;
    [SerializeField]
    List<Transform> listDummySlot;
    [SerializeField]
    List<Transform> listEqmentSlot;
    private bool m_isEqment = false;

    public bool CheckEqment { get => m_isEqment; }

    public void Initialize()
    {
        int nCount = listWeapon.Count;
        listDummySlot = new List<Transform>(nCount);
        listEqmentSlot = new List<Transform>(nCount);
    }

    public void Release()
    {
       for(int i = 0; i<listEqmentSlot.Count; i++ )
       {
            listWeapon[i].parent = listDummySlot[i];
            listWeapon[i].localPosition = Vector3.zero;
            listWeapon[i].localRotation = Quaternion.identity;
            listWeapon[i].localScale = Vector3.one;
            listWeapon[i].gameObject.GetComponent<RayWeapon>().Initialize(null, listDummySlot[i]);

        }
       m_isEqment = false;
    }

    public void Set(Controller playerController)
    {
        for (int i = 0; i < listDummySlot.Count; i++)
        {
            //listWeapon[i].parent = listEqmentSlot[i];
            //listWeapon[i].localPosition = Vector3.zero;
            //listWeapon[i].localRotation = Quaternion.identity;
            //listWeapon[i].localScale = Vector3.one;
            listWeapon[i].gameObject.GetComponent<RayWeapon>().Initialize(playerController, listEqmentSlot[i]);
        }
        m_isEqment = true;
    }

    public void Set()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Tab))
        //{
        //    if (m_isEqment)
        //        Release();
        //    else
        //        Set();
        //}
    }
}

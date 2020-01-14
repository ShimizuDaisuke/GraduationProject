//=======================================================================================
//! @file   CutterController.cs
//! @brief  カッターの動きの処理
//! @author 田中歩夢
//! @date   01月14日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//カッターの動きの処理
public class CutterController : MonoBehaviour
{
    //カッターが当たり判定クラス
    [SerializeField]
    private CutterHitFlag m_cutterHitFlag = default;

    //カッターオブジェクト
    [SerializeField]
    private GameObject m_cutterObj = null;

    //カッターの刃オブジェクト
    [SerializeField]
    private GameObject m_cutterBladeObj = null;

    //始点
    [SerializeField]
    private Transform m_startPos = null;

    //終点
    [SerializeField]
    private Transform m_endPos = null;

    //始点についたかのフラグ
    private bool m_startFlag = false;

    //終点についたかのフラグ
    private bool m_endFlag = false;

    //カッターの刃が出たかフラグ
    private bool m_bladeFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(m_cutterHitFlag.HitFlag)
        {
            m_cutterObj.GetComponent<Rigidbody>().useGravity = false;
            if(!m_startFlag)
            {
                float step = 1.0f * Time.deltaTime;

                m_cutterObj.transform.position = Vector3.MoveTowards(m_cutterObj.transform.position, m_startPos.transform.position, step);

            }
            else
            {
                if (m_cutterBladeObj.transform.localScale.z <= 5)
                {
                    m_cutterBladeObj.transform.localScale = new Vector3(m_cutterBladeObj.transform.localScale.x, m_cutterBladeObj.transform.localScale.y, m_cutterBladeObj.transform.localScale.z + 0.1f);

                }
                else
                {
                    float step = 5.0f * Time.deltaTime;

                    m_cutterObj.transform.position = Vector3.MoveTowards(m_cutterObj.transform.position, m_endPos.transform.position, step);

                }



            }

           
            

            if (m_cutterObj.transform.position == m_startPos.transform.position)
            {
                m_startFlag = true;
            }
            if (m_cutterObj.transform.position == m_endPos.transform.position)
            {
                m_endFlag = true;
            }
        }


        

    }
}

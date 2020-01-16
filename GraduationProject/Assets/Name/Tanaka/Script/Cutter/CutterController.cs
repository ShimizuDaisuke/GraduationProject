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
    //カッターが当たる箱
    [SerializeField]
    private GameObject m_cutterHitBox = default;

    //カッターが当たり判定クラス
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

    //Scaleフラグ
    private bool m_scaleFlag = false;

    //Rotationフラグ
    private bool m_rotationFlag = false;

    //Rigidbody
    private Rigidbody m_cutterObjRigd = default;

    //Box
    private BoxCollider m_cutterObjBox = default;

    //Box消滅カウント
    private float m_boxDeleteCount = 0.0f;

    //Box消滅時間
    private const float BOX_DELETE_TIME = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_cutterHitFlag = m_cutterHitBox.GetComponent<CutterHitFlag>();
        m_cutterObjRigd = m_cutterObj.GetComponent<Rigidbody>();
        m_cutterObjBox = m_cutterObj.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {

        //カッターが当たった
        if(m_cutterHitFlag.HitFlag)
        {
            
            if(!m_startFlag)
            {
                m_cutterObjRigd.useGravity = false;
                m_cutterObjBox.isTrigger = true;
                float step = 0.1f;

                //スタート地点までカッターを移動
                m_cutterObj.transform.position = Vector3.MoveTowards(m_cutterObj.transform.position, m_startPos.transform.position, step);

            }
            else
            {
                if (!m_endFlag)
                {
                    if (!m_scaleFlag)
                    {
                        //刃を拡大
                        if (m_cutterBladeObj.transform.localScale.z <= 5)
                        {
                            m_cutterBladeObj.transform.localScale = new Vector3(m_cutterBladeObj.transform.localScale.x, m_cutterBladeObj.transform.localScale.y, m_cutterBladeObj.transform.localScale.z + 0.1f);

                        }
                        else
                        {
                            m_scaleFlag = true;
                        }
                    }
                    if (!m_rotationFlag)
                    {

                        //カッターを回転
                        if (m_cutterObj.transform.eulerAngles.z <= 134.0f)
                        {
                            float step = 2.0f;
                            m_cutterObj.transform.rotation = Quaternion.RotateTowards(m_cutterObj.transform.rotation, Quaternion.Euler(0, 0, 135.0f), step);

                        }
                        else
                        {
                            m_rotationFlag = true;
                        }
                    }


                    if (m_scaleFlag && m_rotationFlag)
                    {

                        float step = 0.15f;
                        //ゴール地点まで移動
                        m_cutterObj.transform.position = Vector3.MoveTowards(m_cutterObj.transform.position, m_endPos.transform.position, step);

                    }


                }
            }

           
            

            if (m_cutterObj.transform.position == m_startPos.transform.position)
            {
                m_startFlag = true;
            }
            if (m_cutterObj.transform.position == m_endPos.transform.position)
            {
                m_endFlag = true;
                m_cutterObj.GetComponent<Rigidbody>().useGravity = true;
            }

            
        }

        if (m_endFlag)
        {
            m_boxDeleteCount += Time.deltaTime;
        }

        if(m_boxDeleteCount > BOX_DELETE_TIME)
        {
            Destroy(m_cutterHitBox);
           
        }

    }

    //フラグの取得・設定
    public bool EndFlag { get { return m_endFlag; } set { m_endFlag = value; } }

}

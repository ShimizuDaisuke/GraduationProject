//=======================================================================================
//! @file   ScissorsController.cs
//! @brief  ハサミの処理
//! @author 田中歩夢
//! @date   10月21日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ハサミのクラス
public class ScissorsController : MonoBehaviour
{
    //上の刃
    [SerializeField]
    private GameObject m_upperBlade = null;
    //下の刃
    [SerializeField]
    private GameObject m_lowerBlade = null;

    //刃の幅
    [SerializeField]
    private float m_bladeWidth = 30.0f;

    //ハサミのX角度
    [SerializeField]
    private float m_ScissorsAngleX = -30.0f;

    //ハサミのZ角度
    [SerializeField]
    private float m_ScissorsAngleZ = 20.0f;

    //切る速度
    [SerializeField]
    private float m_cutSpeed = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        //初期Y角度の設定
        m_upperBlade.transform.rotation = Quaternion.Euler(m_ScissorsAngleX, 90.0f - m_bladeWidth, m_ScissorsAngleZ);
        m_lowerBlade.transform.rotation = Quaternion.Euler(m_ScissorsAngleX, 90.0f + m_bladeWidth, -m_ScissorsAngleZ);

    }

    // Update is called once per frame
    void Update()
    {
        //切る動き
        MoveCut();
    }


    //切る動きの処理
    private void MoveCut()
    {

        //上の刃のY角度を動かす
        if(m_upperBlade.transform.eulerAngles.y <= 90.0f)
        {
            m_upperBlade.transform.rotation = Quaternion.Euler(m_upperBlade.transform.eulerAngles.x, m_upperBlade.transform.eulerAngles.y + m_cutSpeed, m_upperBlade.transform.eulerAngles.z);
        }
        //下の刃のY角度を動かす
        if (m_lowerBlade.transform.eulerAngles.y >= 90.0f)
        {
            m_lowerBlade.transform.rotation = Quaternion.Euler(m_lowerBlade.transform.eulerAngles.x, m_lowerBlade.transform.eulerAngles.y - m_cutSpeed, m_lowerBlade.transform.eulerAngles.z);
        }

        //上の刃のZ角度を動かす
        if (m_upperBlade.transform.eulerAngles.z >= 1.0f)
        {
            m_upperBlade.transform.rotation = Quaternion.Euler(m_upperBlade.transform.eulerAngles.x, m_upperBlade.transform.eulerAngles.y, m_upperBlade.transform.eulerAngles.z - m_cutSpeed);
        }
        Debug.Log(m_lowerBlade.transform.eulerAngles.z);
        //下の刃のZ角度を動かす
        if (m_lowerBlade.transform.eulerAngles.z <= 358.0f)
        {
            Debug.Log(m_lowerBlade.transform.eulerAngles.z);
            m_lowerBlade.transform.rotation = Quaternion.Euler(m_lowerBlade.transform.eulerAngles.x, m_lowerBlade.transform.eulerAngles.y, m_lowerBlade.transform.eulerAngles.z + m_cutSpeed);
        }

        //もしOより下に行ったら0にする
        if(m_lowerBlade.transform.eulerAngles.z <= 0.0f)
        {
            m_lowerBlade.transform.rotation = Quaternion.Euler(m_lowerBlade.transform.eulerAngles.x, m_lowerBlade.transform.eulerAngles.y, 0.0f);
        }

    }
}

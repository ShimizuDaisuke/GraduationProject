//=======================================================================================
//! @file   BalanceController.cs
//! @brief  天秤の処理
//! @author 田中歩夢
//! @date   12月16日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//天秤の処理
public class BalanceController : MonoBehaviour
{
    //モータークラス
    [SerializeField]
    private MotorController m_motorCon = default;

    //鉄側の板の当たり判定クラス
    [SerializeField]
    private BoardIronSideController m_boradIronSideCon = default;

    //鉄側の板
    [SerializeField]
    private GameObject m_board_IronSide = null;

    //ビー玉側の板
    [SerializeField]
    private GameObject m_board_MarbleSide = null;

    //鉄側の板の子供
    private GameObject m_childIronSide = null;

    //鉄側の板の孫
    private GameObject m_grandchildIronSide = null:

    // Start is called before the first frame update
    void Start()
    {
        m_childIronSide = m_board_IronSide.transform.GetChild().gameObject
    }

    // Update is called once per frame
    void Update()
    {

        //回路がつながってる時
        if (m_motorCon.MotorConConnectFlag)
        {
            if(!m_boradIronSideCon.HitFlag)
            {
                m_board_IronSide.transform.localScale = new Vector3(m_board_IronSide.transform.localScale.x, m_board_IronSide.transform.localScale.y + 0.01f, m_board_IronSide.transform.localScale.z);
                m_board_MarbleSide.transform.localScale = new Vector3(m_board_MarbleSide.transform.localScale.x, m_board_MarbleSide.transform.localScale.y - 0.01f, m_board_MarbleSide.transform.localScale.z);
            }
            else
            {
                
            }

        }


    }
}

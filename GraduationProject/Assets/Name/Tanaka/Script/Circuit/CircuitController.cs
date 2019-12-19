//=======================================================================================
//! @file   CircuitController.cs
//! @brief  回路の動き
//! @author 田中歩夢
//! @date   12月04日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//回路のクラス
public class CircuitController : MonoBehaviour
{
    //プレイヤーの属性
    [SerializeField]
    private PlayerType m_playerType = default;

    //バッテリークラス
    [SerializeField]
    private MotorController m_motorCon = default;

    //回路がつながったフラグ
    private bool m_connectFlag = false;

    //衝突判定
    void OnTriggerStay(Collider collider)
    {
        //アルミ箔が当たったら
        if (collider.gameObject.tag == "AluminumFoil")
        {
            m_connectFlag = true;

        }

        //プレイヤーが鉄の状態で当たったら
        if(m_playerType.IsPlayerType == PlayerType.Type.IRON)
        {
            if(collider.gameObject.tag == "Player")
            {
                m_connectFlag = true;
            }
        }

    }

    //衝突判定
    void OnTriggerExit(Collider collider)
    {
        //アルミ箔が出て行ったら
        if (collider.gameObject.tag == "AluminumFoil")
        {
            m_connectFlag = false;
            m_motorCon.MotorConConnectFlag = false;

        }

        //プレイヤーが鉄の状態で当たったら
        if (m_playerType.IsPlayerType == PlayerType.Type.IRON)
        {
            if (collider.gameObject.tag == "Player")
            {
                m_connectFlag = false;
                m_motorCon.MotorConConnectFlag = false;
            }
        }

    }


    //回路がつながったフラグの取得・設定
    public bool ConnectFlag { get { return m_connectFlag; } set { m_connectFlag = value; } }

}

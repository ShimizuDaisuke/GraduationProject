//=======================================================================================
//! @file   PlayerType.cs
//! @brief  プレイヤーの属性の処理
//! @author 田中歩夢
//! @date   11月28日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerType : MonoBehaviour
{
    //プレイヤーの属性
    public enum Type
    {
        ERASER,         //消しゴム
        IRON,           //鉄
        MARBLE,         //ビー玉
        MAX_TYPE,       // 最大数
    };

    //プレイヤーの属性
    [SerializeField]
    private Type m_playerType = Type.ERASER;

    //プレイヤーディレクター
    [SerializeField]
    private PlayerController m_playerCon = default;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //プレイヤーの属性の取得・設定
    public Type IsPlayerType
    {
        get { return m_playerType; }
        set
        {
            m_playerType = value;
            m_playerCon.ChangeVel();
        }
    }

}

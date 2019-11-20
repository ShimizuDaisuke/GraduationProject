//=======================================================================================
//! @file   MoveStopCol.cs
//! @brief  当たったプレイヤーの動きを止めるフラグの処理
//! @author 田中歩夢
//! @date   11月19日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//当たったプレイヤーの動きを止めるフラグのクラス
public class MoveStopCol : MonoBehaviour
{
    //プレイヤー
    private GameObject m_player;

    //移動止めるフラグ
    private bool m_moveStopFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        // プレイヤーを探す
        m_player = GameObject.FindGameObjectWithTag("Player");
    }

    //衝突判定
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            m_moveStopFlag = true;
        }
    }


    //移動止めるフラグの取得・設定
    public bool MoveStopFlag { get { return m_moveStopFlag; } set { m_moveStopFlag = value; } }

}

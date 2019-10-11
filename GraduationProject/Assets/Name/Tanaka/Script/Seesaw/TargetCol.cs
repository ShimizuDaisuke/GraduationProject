//=======================================================================================
//! @file   TargetCol.cs
//! @brief  ターゲットの当たり判定処理
//! @author 田中歩夢
//! @date   10月10日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ターゲットの当たり判定クラス
public class TargetCol : MonoBehaviour
{
    //イベント管理クラス
    [SerializeField]
    private EventDirector m_event = default;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //衝突判定
    void OnTriggerEnter(Collider collider)
    {
        //投げられたとき
        if(m_event.IsEventKIND == EventDirector.EventKIND.RULE_THOW)
        {
            //プレイヤーがターゲット座標にたどり着いたか
            if(collider.gameObject.tag == "Player")
            {
                m_event.IsEventKIND = EventDirector.EventKIND.NONE;
            }
        }

    }

}

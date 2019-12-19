//=======================================================================================
//! @file   HitEraseEvent.cs
//! @brief  当たったら落書きを消す処理
//! @author 田中歩夢
//! @date   10月16日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//当たったら落書きを消すクラス
public class HitEraseEvent : MonoBehaviour
{
    //イベントクラス
    [SerializeField]
    private EventDirector m_event = default;

    // ヒットフラグ
    private bool m_hitFlag = false;

    //衝突判定
    void OnTriggerEnter(Collider collider)
    {
        if(!m_hitFlag)
        {
            if (collider.gameObject.tag == "Player")
            {
                //直進移動のイベントに設定
                m_event.IsEventKIND = EventDirector.EventKIND.NOTEBOOK_GRAFFITI_ERASE;
                m_hitFlag = true;
            }
        }
        
    }

    public bool HitFlag { get { return m_hitFlag; } set { m_hitFlag = value; } }

}

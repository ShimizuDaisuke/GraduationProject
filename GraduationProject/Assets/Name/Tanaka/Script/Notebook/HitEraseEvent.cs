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
    private EventDirector m_event;

    //プレイヤーのゲームオブジェクト
    private GameObject m_player = null;

    // 
    private bool flag = false;

    //衝突判定
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            //直進移動のイベントに設定
            m_event.IsEventKIND = EventDirector.EventKIND.NOTEBOOK_GRAFFITI_ERASE;
            flag = true;
        }
    }

    public bool HitFlag { get { return flag; } set { flag = value; } }

}

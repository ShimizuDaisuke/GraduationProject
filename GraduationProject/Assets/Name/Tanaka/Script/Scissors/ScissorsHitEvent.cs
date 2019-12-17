//=======================================================================================
//! @file   ScissorsHitEvent.cs
//! @brief  ハサミに当たったらイベント
//! @author 田中歩夢
//! @date   10月24日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ハサミ当たったらイベントクラス
public class ScissorsHitEvent : MonoBehaviour
{
    //イベントクラス
    [SerializeField]
    private EventDirector m_event = default;

    // ヒットフラグ
    private bool m_hitFlag = false;

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
        if (!m_hitFlag)
        {
            if (collider.gameObject.tag == "Player")
            {
                //直進移動のイベントに設定
                m_event.IsEventKIND = EventDirector.EventKIND.SCISSORS_CUT;
                m_hitFlag = true;
            }
        }

    }

    public bool HitFlag { get { return m_hitFlag; } set { m_hitFlag = value; } }

}

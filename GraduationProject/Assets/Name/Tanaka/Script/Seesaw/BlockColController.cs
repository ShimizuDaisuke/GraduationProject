//=======================================================================================
//! @file   BlockColController.cs
//! @brief  横からの侵入をブロックする当たり判定の処理
//! @author 田中歩夢
//! @date   10月11日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//横からの侵入をブロックする当たり判定クラス
public class BlockColController : MonoBehaviour
{
    //イベントクラス
    [SerializeField]
    private EventDirector m_event = default;

    //ブロックする当たり判定オブジェクト
    [SerializeField]
    private GameObject m_blockObj = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //直進移動の時と投げるときは消す
        if(m_event.IsEventKIND == EventDirector.EventKIND.RULE_MOVE_STRAIGHT || m_event.IsEventKIND == EventDirector.EventKIND.RULE_THOW)
        {
            m_blockObj.SetActive(false);
        }
        else
        {
            //m_blockObj.SetActive(true);
        }

    }
}

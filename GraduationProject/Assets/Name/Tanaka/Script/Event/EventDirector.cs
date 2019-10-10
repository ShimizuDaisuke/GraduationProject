//=======================================================================================
//! @file   EventDirector.cs
//! @brief  イベントの管理の処理
//! @author 田中歩夢
//! @date   10月10日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//イベントの管理クラス
public class EventDirector : MonoBehaviour
{
    //イベント
    public enum EventKIND
    {
        ERR = -1,       //エラー
        None,           //何も起きていない
        Thow,           //投げられた
    };

    //イベント
    [SerializeField]
    private EventKIND m_event = EventKIND.None;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //イベントの取得・設定
    public EventKIND IsEventKIND { get { return m_event; } set { m_event = value; } }

}

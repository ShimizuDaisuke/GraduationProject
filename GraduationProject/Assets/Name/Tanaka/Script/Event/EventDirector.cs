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
        ERR = -1,                   //エラー
        NONE,                       //何も起きていない
        RULE_THOW,                  //投げられた
        RULE_MOVE_STRAIGHT,         //直進移動
        RULE_DOMINO,                //ドミノ倒し
        NOTEBOOK_GRAFFITI_ERASE,    //ノートの落書きを消す
        RULE_QR,                    //QRコード読み込み
        RULE_CUTTERKNIFE,            //カッターナイフをしまう
        SCISSORS_CUT,               //ハサミ切る
        MAX,                        //最大イベント数
    };
   
    //イベント
    [SerializeField]
    private EventKIND m_event = EventKIND.NONE;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(m_event);
    }

    //イベントの取得・設定
    public EventKIND IsEventKIND { get { return m_event; } set { m_event = value; } }

}

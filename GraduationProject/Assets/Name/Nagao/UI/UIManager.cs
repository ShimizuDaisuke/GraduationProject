//======================================================================================= 
//! @file       UIManager 
//! @brief      何かしらのイベントが始まったらUIの非表示 
//! @author     長尾昌輝 
//! @date       2019/10/23 
//! @note       メモ  ※書かなくてもいい 
//======================================================================================= 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    //イベント管理クラス
    [SerializeField]
    private EventDirector m_event = default;

    //シーンのタイム
    [SerializeField]
    private GameObject TimeDirector = default;

    //時計の処理
    private TimerController m_timerController;

    //通常UIのオブジェクト
    [SerializeField]
    private GameObject NormalUI = default;

    //EventUIのオブジェクト
    [SerializeField]
    private GameObject EventUI = default;

    //QR用UIのオブジェクト
    [SerializeField]
    private GameObject QRUI = default;

    //TimeUIオブジェクト
    [SerializeField]
    private GameObject m_timeUI = default;

    //QRTimeStop
    [SerializeField]
    private QRTimeStop m_qrTimeStop = default;

    

    // Start is called before the first frame update
    void Start()
    {
        m_timerController = TimeDirector.GetComponent<TimerController>(); 
    }

    // Update is called once per frame
    void Update()
    { 
        // 筆箱に入るイベントならリターン
        if(m_event.IsEventKIND == EventDirector.EventKIND.PENCILCASE_MOVE_GOOL) return;

        //イベント中か
        if ((m_event.IsEventKIND != EventDirector.EventKIND.NONE))
        {
            
            //通常UIの非表示
            NormalUI.SetActive(false);

            //ＱＲコードを読み込む以外のイベントか
            if ((m_event.IsEventKIND != EventDirector.EventKIND.RULE_QR))
            {
                //時間UIの表示
                m_timeUI.SetActive(true);

                //イベント用UIの表示
                EventUI.SetActive(true);
                //QR用UIの非表示
                QRUI.SetActive(false);
            }
            else
            {
                //時間UIの表示
                m_timeUI.SetActive(false);

                //イベント用UIの表示
                EventUI.SetActive(false);
                //QR用UIの非表示
                QRUI.SetActive(true);

                //ゲーム内の時を止める
                m_timerController.TimerFlag = false;
            }

        }
        else
        {
            //イベント中ではない時

            //通常UIの表示
            NormalUI.SetActive(true);
            //イベント用UIの非表示
            EventUI.SetActive(false);
            //QR用UIの非表示
            QRUI.SetActive(false);
            //時間UIの表示
            m_timeUI.SetActive(true);

            if (m_qrTimeStop.TimeStop == QRTimeStop.TIMESTOP_COUNT.NONE)
                //ゲーム内の時を動かす
                m_timerController.TimerFlag = true;
            else
                m_timerController.TimerFlag = false;

        }

        

    }
}

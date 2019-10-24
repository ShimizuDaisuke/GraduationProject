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
    private GameObject UI = default;

    //EventUIのオブジェクト
    [SerializeField]
    private GameObject EventUI = default;


    // Start is called before the first frame update
    void Start()
    {
        m_timerController = TimeDirector.GetComponent<TimerController>(); 
    }

    // Update is called once per frame
    void Update()
    {
        //イベントが始まっていないか
        if ((m_event.IsEventKIND != EventDirector.EventKIND.NONE))
        {
            UI.SetActive(false);
            EventUI.SetActive(true);
            m_timerController.TimerFlag = false;
        }
        else
        {
            //イベント中
            UI.SetActive(true);
            EventUI.SetActive(false);
            m_timerController.TimerFlag = true;
        }
    }
}

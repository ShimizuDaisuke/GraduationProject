//=======================================================================================
//! @file   QRTimeStop.cs
//! @brief  QR時間停止の処理
//! @author 田中歩夢
//! @date   01月21日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// QR時間停止の処理
public class QRTimeStop : MonoBehaviour
{
    //時間停止
    public enum TIMESTOP_COUNT
    {
        NONE    = 0,
        STOP5   = 5,
        STOP10  = 10,
        UNDO_TIME = 1,
        NUM,
    };

    public TIMESTOP_COUNT m_timeStopCount = TIMESTOP_COUNT.NONE;

    //タイマーController
    [SerializeField]
    private TimerController m_timerCon = default;

    //カウント
    private float m_count = 0.0f;

    //画面を元に戻すフラグ
    private bool m_undoTimeFlag = false;

    //カウントが開始しだしたか
    private bool m_countUpFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(m_timeStopCount == TIMESTOP_COUNT.NONE)
            return;
        else
        {
            if(!m_countUpFlag)
            {
                m_countUpFlag = true;
                m_count = 0.0f;
            }
        }

        if(m_countUpFlag)
        {
            //時間停止
            m_timerCon.TimerFlag = false;

            //止める時間
            int maxTime = (int)m_timeStopCount;

            if (m_count > maxTime)
            {
                //戻す
                m_timerCon.TimerFlag = true;
                //画面を元に戻すための時間 
                if (!m_undoTimeFlag)
                {
                    m_timeStopCount = TIMESTOP_COUNT.UNDO_TIME;
                    m_count = 0.0f;
                    m_undoTimeFlag = true;
                }
                else
                {
                    m_timeStopCount = TIMESTOP_COUNT.NONE;
                    m_count = 0.0f;
                    m_undoTimeFlag = false;
                }


            }
            else
            {
                m_count += Time.deltaTime;
            }
        }
        

        if(m_count < 0.0f)
        {
            m_count = 0.0f;
        }

    }


    public TIMESTOP_COUNT TimeStop { get { return m_timeStopCount; } set { m_timeStopCount = value; } }


    public float Count { get { return m_count; } set { m_count = value; } }
}

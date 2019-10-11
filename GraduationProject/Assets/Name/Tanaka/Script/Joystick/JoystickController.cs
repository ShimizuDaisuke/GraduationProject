//=======================================================================================
//! @file   Joystick.cs
//! @brief  ジョイスティックの処理
//! @author 田中歩夢
//! @date   10月04日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickController : MonoBehaviour
{
    //ゲームオブジェクトのジョイスティック
    [SerializeField]
    private GameObject m_joystick = default;

    //2Dカメラ ↔ 3Dカメラへ動くクラス
    [SerializeField]
    private CameraDirector m_cameradirector = default;

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
        //２Dと３Dのカメラの切り替え中フラグ
        bool cameraSwitch2D3D = m_cameradirector.IsMove2D3DCameraPos;

        //２Dと３Dのカメラの切り替え中かどうか
        if ((!cameraSwitch2D3D) && (m_event.IsEventKIND == EventDirector.EventKIND.NONE))
        {
            m_joystick.SetActive(true);
        }
        else
        {
            m_joystick.SetActive(false);
        }
    }
}

//=======================================================================================
//! @file   EventCameraGoalFromStart.cs
//! @brief  イベントカメラ　スタートからゴールの動きの処理
//! @author 田中歩夢
//! @date   01月21日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//イベントカメラ　スタートからゴールの動きの処理
public class EventCameraGoalFromStart : CameraEventBase
{
    //イベントDirector
    [SerializeField]
    private EventDirector m_eventDirector = default;

    //フェード
    [SerializeField]
    private Fade m_fade = default;

    //カメラのスタートの座標
    [SerializeField]
    private Transform m_cameraStartPos =default;

    //カメラのゴールの座標
    [SerializeField]
    private Transform m_cameraGoalPos = default;

    //カメラの移動速度
    [SerializeField]
    private float m_cameraMoveSpeed = 0.5f;

    //ゴールの座標から引いた
    [SerializeField]
    private float m_goalDifferenceX = 0.0f;

    //カメラDirector
    private CameraDirector m_cameraDirector = default;

    //スタート座標についたかフラグ
    private bool m_startFlag = false;

    //ゴール座標についたかフラグ
    private bool m_goalFlag = false;

    //終了フラグ
    private bool m_finishFlag = false;

    //初期座標_3Dカメラ
    private Vector3 m_initialPos_Cam3D = default;

    void Awake()
    {
        // 初期化する
        Initilaize();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_initialPos_Cam3D = Camera3D.transform.position;
        m_cameraDirector = GetComponent<CameraDirector>();
    }

    public override void MoveCameraByEvent()
    {
        //スタート座標に行く
        if(!m_startFlag)
        {
            //カメラの回転
            Camera3D.transform.rotation = Quaternion.Euler(40.0f, 90.0f, 0.0f);
            //スタート座標に移動
            Camera3D.transform.position = m_cameraStartPos.transform.position;
            m_startFlag = true;  
        }
        else
        {
            //フェードインが終わってゴールにたどり着いていなかったら
            if (!m_fade.FadeIn && !m_goalFlag)
            {
                //スタート座標からゴール座標へ
                Camera3D.transform.position = Vector3.MoveTowards(Camera3D.transform.position, m_cameraGoalPos.transform.position, m_cameraMoveSpeed);
            }
        }

        if(!m_goalFlag)
        {
            //カメラがゴールから少し前にたどり着いたらフェードアウト
            if (Camera3D.transform.position.x >= m_cameraGoalPos.transform.position.x - m_goalDifferenceX)
            {
                m_fade.FadeOut = true;
                m_goalFlag = true;
            }
        }

        //イベントを終了してもとに戻す
        if (m_goalFlag && !m_fade.FadeOut && !m_finishFlag)
        {
            //3Dカメラを初期位置に戻す
            Camera3D.transform.position = m_initialPos_Cam3D;

            //カメラの回転
            Camera3D.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);

            //カメラを2Dにする
            m_cameraDirector.ChangeCamera2D3D();

            m_fade.FadeIn = true;
            m_finishFlag = true;
        }

        //イベント終了
        if (m_goalFlag && !m_fade.FadeIn && m_finishFlag)
        {
            m_eventDirector.IsEventKIND = EventDirector.EventKIND.NONE;
        }
        
    }
}

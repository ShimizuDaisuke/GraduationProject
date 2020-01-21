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
    //[SerializeField]
    //private m_cameraStartPos =default;


    void Awake()
    {
        // 初期化する
        Initilaize();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void MoveCameraByEvent()
    {

        //Camera3D.transform.position = Vector3.MoveTowards(Camera3D.transform.position, m_cameraMovePos.transform.position, m_cameraMoveSpeed);

        ////目標角度まで回転する
        //if (Camera3D.transform.localEulerAngles.x <= m_targetAngle)
        //{
        //    Camera3D.transform.RotateAround(Player.transform.position, Vector3.forward, -m_angleSpeed);
        //}
        //else
        //{
        //    m_moveFlag = true;
        //}
    }
}

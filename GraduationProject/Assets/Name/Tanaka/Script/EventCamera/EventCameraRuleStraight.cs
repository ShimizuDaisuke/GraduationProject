//=======================================================================================
//! @file   EventCameraRuleStraight.cs
//! @brief  投げる前のカメラの処理
//! @author 田中歩夢
//! @date   10月25日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//投げる前のカメラの動きのクラス
public class EventCameraRuleStraight : CameraEventBase
{
    //2Dカメラ ↔ 3Dカメラへ動くクラス
    private CameraDirector m_cameraDirector = default;

    //カメラの移動先座標
    [SerializeField]
    private GameObject m_targetPosObj;

    //目標の角度
    [SerializeField]
    private float m_targetAngle = 80.0f;

    //回転速度
    [SerializeField]
    private float m_angleSpeed = 0.5f;

    //ターゲットの座標に進む速度
    [SerializeField]
    private float m_moveSpeed = 0.06f;

    //カメラの２Dの移動速度
    [SerializeField]
    private float m_camera2DMoveSpeed = 0.1f;

    //二点間の距離を入れる
    private float distance_two;

 
    void Awake()
    {
        // 初期化する
        Initilaize();
    }

    // Start is called before the first frame update
    void Start()
    {

        //二点間の距離を代入(スピード調整に使う)
        distance_two = Vector3.Distance(Camera3D.transform.position, m_targetPosObj.transform.position);

        //2Dカメラ ↔ 3Dカメラへ動くクラスを取得
        m_cameraDirector = gameObject.GetComponent<CameraDirector>();
    }


    /// <summary>
    /// イベント用にカメラを動かす
    /// </summary>
    public override void MoveCameraByEvent()
    {
        //２Dと３Dのカメラの切り替え中フラグ
        bool camera2Dor3DFlag = m_cameraDirector.IsAppearCamera3D;

        //2Dカメラの時
        if (!m_cameraDirector.IsAppearCamera3D)
        {
            Vector3 camera2DTargetPos = new Vector3(Player.transform.position.x, Player.transform.position.y, Camera2D.transform.position.z);
            Camera2D.transform.position = Vector3.MoveTowards(Camera2D.transform.position, camera2DTargetPos, m_camera2DMoveSpeed);
        }
        //3Dカメラの時
        else
        {
            //目標角度まで回転する
            if (Camera3D.transform.localEulerAngles.y >= m_targetAngle)
            {
                Camera3D.transform.RotateAround(Player.transform.position, Vector3.up, -m_angleSpeed);
            }

            // 現在の位置
            float present_Location = (Time.time * m_moveSpeed) / distance_two;

            //カメラの移動
            Camera3D.transform.position = Vector3.Lerp(Camera3D.transform.position, m_targetPosObj.transform.position, present_Location);
        }

    }
}

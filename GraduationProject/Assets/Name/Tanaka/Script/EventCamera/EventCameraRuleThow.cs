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
public class EventCameraRuleThow : CameraEventBase
{
    //2Dカメラ ↔ 3Dカメラへ動くクラス
    private CameraDirector m_cameraDirector = default;

    //カメラの移動先座標
    [SerializeField]
    private GameObject m_targetPosObj;

    //目標のX角度
    [SerializeField]
    private float m_targetAngleX = 30.0f;

    //目標のY角度
    [SerializeField]
    private float m_targetAngleY = 90.0f;

    //回転速度X軸
    [SerializeField]
    private float m_angleSpeedX = 1.0f;

    //回転速度Y軸
    [SerializeField]
    private float m_angleSpeedY = 0.5f;

    //カメラの移動速度
    [SerializeField]
    private float m_cameraMoveSpeed = 1.0f;

    void Awake()
    {
        // 初期化する
        Initilaize();
    }

    // Start is called before the first frame update
    void Start()
    {
        //2Dカメラ ↔ 3Dカメラへ動くクラスを取得
        m_cameraDirector = gameObject.GetComponent<CameraDirector>();
    }


    /// <summary>
    /// イベント用にカメラを動かす
    /// </summary>
    /// <param name="camera3D">3D空間上に映すカメラ(ref:位置や回転などを変えられる)</param>
    /// <param name="camera2D">2D空間上に映すカメラ(ref:位置や回転などを変えられる)</param>
    public override void MoveCameraByEvent()
    {

        //２Dと３Dのカメラの切り替え中フラグ
        bool camera2Dor3DFlag = m_cameraDirector.IsAppearCamera3D;

        //2Dカメラの時
        if (!m_cameraDirector.IsAppearCamera3D)
        {
            Camera2D.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, Camera2D.transform.position.z);

        }
        //3Dカメラの時
        else
        {
            //カメラの座標調整
            Vector3 targetPos = new Vector3(Player.transform.position.x - 8, Player.transform.position.y + 2.5f, Player.transform.position.z - 1.0f);
            Camera3D.transform.position = Vector3.MoveTowards(Camera3D.transform.position, targetPos, m_cameraMoveSpeed);

            //カメラのX軸回転
            if (Camera3D.transform.localEulerAngles.x <= m_targetAngleX)
            {
                Camera3D.transform.RotateAround(Player.transform.position, Vector3.forward, -m_angleSpeedX);
            }

            //カメラのY軸回転
            if (Camera3D.transform.localEulerAngles.y <= m_targetAngleY)
            {
                Camera3D.transform.RotateAround(Player.transform.position, Vector3.up, m_angleSpeedY);
            }

            //カメラのZ軸を0で固定
            Camera3D.transform.rotation = Quaternion.Euler(new Vector3(Camera3D.transform.localEulerAngles.x, Camera3D.transform.localEulerAngles.y, 0.0f));

        }

    }
}

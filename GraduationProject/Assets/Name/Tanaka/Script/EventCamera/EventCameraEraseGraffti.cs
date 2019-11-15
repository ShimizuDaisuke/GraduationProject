//=======================================================================================
//! @file   EventCameraEraseGraffti.cs
//! @brief  落書きを消す時のカメラの処理
//! @author 田中歩夢
//! @date   10月25日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//落書きを消す時のカメラのクラス
public class EventCameraEraseGraffti : CameraEventBase
{

    //落書きのレイヤーマスク
    [SerializeField]
    private LayerMask m_layerMaskGraffti;

    //目標の角度
    [SerializeField]
    private float m_targetAngle = 80.0f;

    //回転速度
    [SerializeField]
    private float m_angleSpeed = 0.5f;

    //カメラの移動速度
    [SerializeField]
    private float m_cameraMoveSpeed = 0.5f;

    //動き切ったかどうか
    private bool m_moveFlag = false;


    void Awake()
    {
        // 初期化する
        Initilaize();

       
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    /// <summary>
    /// イベント用にカメラを動かす
    /// </summary>
    public override void MoveCameraByEvent()
    {
        m_moveFlag = false;
        //レイの作成
        Ray ray = new Ray(Player.transform.position, Vector3.right + (Vector3.up * - 1));

        // Rayが衝突したコライダーの情報
        RaycastHit hit;

        
        //レイの飛ばす距離
        float distance = 10.0f;
        Debug.DrawRay(ray.origin, ray.direction * distance,Color.red);

        if (Physics.Raycast(ray, out hit, distance, m_layerMaskGraffti))
        {
            //親を取得
            GameObject grafftiParent = hit.collider.transform.parent.gameObject;

            //カメラを指定位置まで移動
            GameObject cameraMovePos = grafftiParent.transform.Find("CameraMovePos").gameObject;
            Camera3D.transform.position = Vector3.MoveTowards(Camera3D.transform.position, cameraMovePos.transform.position, m_cameraMoveSpeed);

            //目標角度まで回転する
            if (Camera3D.transform.localEulerAngles.x <= m_targetAngle)
            {
                Camera3D.transform.RotateAround(Player.transform.position, Vector3.forward, -m_angleSpeed);
            }
            else
            {
                m_moveFlag = true;
            }
        }

    }

    public bool MoveFlag { get { return m_moveFlag; } set { m_moveFlag = value; } }

}

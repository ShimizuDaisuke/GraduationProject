//=======================================================================================
//! @file   PlayerController.cs
//! @brief  プレイヤー操作の処理
//! @author 田中歩夢
//! @date   10月10日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤーの操作クラス
public class PlayerController : MonoBehaviour
{
    //2Dカメラ ↔ 3Dカメラへ動くクラス
    [SerializeField]
    private CameraDirector m_cameradirector = default;

    //イベント管理クラス
    [SerializeField]
    private EventDirector m_event = default;

    //ジョイスティッククラス
    [SerializeField]
    private Joystick m_joystick = default;

    //速度
    [SerializeField]
    private float m_vel = default;

    // プレイヤーの動きを制限
    bool IsMove = true;

    // プレイヤーの動きを制限する時間
    float time = 0.0f;

    //ジョイスティックで動かした方向
    private float m_dx, m_dy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //移動処理
        Move();
    }

    //移動処理
    private void Move()
    {
        //２Dと３Dのカメラの切り替え中フラグ
        bool cameraSwitch2D3D = m_cameradirector.IsMove2D3DCameraPos;

        //２Dと３Dのカメラの切り替え中かどうか,イベントが何も起きていないか
        if ((!cameraSwitch2D3D) && (m_event.IsEventKIND == EventDirector.EventKIND.NONE))
        {

            //ジョイスティックで動かした方向を取得
            m_dx = m_joystick.Horizontal;
            m_dy = m_joystick.Vertical;


            //2Dカメラか3Dカメラかのフラグ
            bool camera2Dor3DFlag = m_cameradirector.IsAppearCamera3D;
           
            //2Dカメラの時
            if (!camera2Dor3DFlag)
            {
                //2Dの移動 Addforceで動かす
                transform.Translate(m_dx * m_vel, 0.0f, 0.0f);

                //Rigidbody rig = GetComponent<Rigidbody>();
                //rig.AddForce(dx * m_vel, 0.0f, 0.0f);
            }
            //3Dカメラの時
            else
            {
                //3Dの移動 Addforceで動かす
                transform.Translate(m_dy * m_vel, 0.0f, -m_dx * m_vel);

                //Rigidbody rig = GetComponent<Rigidbody>();
                //rig.AddForce(dy * m_vel, 0.0f, -dx * m_vel);
            }

            
        }
    }

    public float DX { get { return m_dx; } set { m_dx = value; } }
    public float DY { get { return m_dy; } set { m_dy = value; } }


}

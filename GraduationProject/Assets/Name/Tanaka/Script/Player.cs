//=======================================================================================
//! @file   Player.cs
//! @brief  プレイヤーの処理
//! @author 田中歩夢
//! @date   9月27日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤークラス
public class Player : MonoBehaviour
{
    //ジェイスティック
    [SerializeField]
    private Joystick m_joystick = default;
    //速度
    [SerializeField]
    private float m_vel = default;
    //2Dカメラ ↔ 3Dカメラへ動くクラス
    [SerializeField]
    private CameraDirector m_cameradirector = default;

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
        if (!cameraSwitch2D3D)
        {
            //ジョイスティックで動かした方向
            float dx = m_joystick.Horizontal;
            float dy = m_joystick.Vertical;

            //2Dカメラか3Dカメラかのフラグ
            bool camera2Dor3DFlag = m_cameradirector.IsAppearCamera3D;
            //2Dカメラの時
            if (!camera2Dor3DFlag)
            {
                //2Dの移動
                transform.Translate(dx * m_vel, 0.0f, 0.0f);
            }
            //3Dカメラの時
            else
            {
                //3Dの移動
                transform.Translate(dy * m_vel, 0.0f, -dx * m_vel);
            }
        }
        
    }

    
}

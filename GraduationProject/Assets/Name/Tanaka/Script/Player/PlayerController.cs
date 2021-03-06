﻿//=======================================================================================
//! @file   PlayerController.cs
//!
//! @brief  プレイヤー操作の処理
//!
//! @author 田中歩夢、橋本奉武
//!
//! @date   10月10日
//!
//! @note   「PlayerDirector」オブジェクトにアタッチする
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤーの操作クラス
public class PlayerController : MonoBehaviour
{
    //　カメラの監督
    [SerializeField]
    private GameObject m_cameradirector = default;

    //イベント管理クラス
    [SerializeField]
    private EventDirector m_event = default;

    //ジョイスティッククラス
    [SerializeField]
    private Joystick m_joystick = default;

    //消しゴムの速度
    [SerializeField]
    private float m_eraserVel = default;
    //鉄の速度
    [SerializeField]
    private float m_ironVel = default;
    //ビー玉の速度
    [SerializeField]
    private float m_marbleVel = default;

    //速度 <目安:150>
    private float m_vel = default;

    // 最大速度<目安:4.0>
    [SerializeField]
    private float m_maxvel = default;

    //プレイヤーの属性
    [SerializeField]
    private PlayerType m_playerType = default;

    // プレイヤー
    private GameObject PlayerObj = null;

    // プレイヤーのRigidbody
    private Rigidbody m_rigidbody = default;

    // 前のプレイヤーの速度；
    private Vector3 oncemovevelocity = Vector3.zero;

    //ジョイスティックで動かした方向X
    private Vector2 m_d = Vector2.zero;

    // スクリプト： 2D↔3Dへカメラが動く
    private CameraDirector s_cameradirector;

    // スクリプト： カメラがプレイヤーに追従する
    private CameraFollowPlayer s_camerafollowplayer;

    // Start is called before the first frame update
    void Start()
    {
        // プレイヤーを探す
        PlayerObj = GameObject.FindGameObjectWithTag("Player");

        // プレイヤーのRigidbodyを探す
        m_rigidbody = PlayerObj.transform.GetComponent<Rigidbody>();

        // 速度を初期化
        m_vel = m_eraserVel;

        // スクリプト： 2D↔3Dへカメラが動く 取得
        s_cameradirector = m_cameradirector.GetComponent<CameraDirector>();

        // スクリプト： カメラがプレイヤーに追従する 取得
        s_camerafollowplayer = m_cameradirector.GetComponent<CameraFollowPlayer>();
    } 

    // PlayerDirector.cs の Update に移動
    //void Update()
    //{
    //    //移動処理
    //    Move();
    //}

    /// <summary>
    /// プレイヤーによる移動処理
    /// </summary>
    public void Move()
    {
        //2Dカメラか3Dカメラかのフラグ
        bool camera2Dor3DFlag = s_cameradirector.IsAppearCamera3D;
       

        // 2Dと3Dのカメラの切り替え中フラグ
        bool cameraSwitch2D3D = s_cameradirector.IsMove2D3DCameraPos;

        //2Dと3Dのカメラの切り替え中かどうか,イベントが何も起きていないか
        if ((!cameraSwitch2D3D) && (m_event.IsEventKIND == EventDirector.EventKIND.NONE))
        {
            //ジョイスティックで動かした方向
            m_d.x = m_joystick.Horizontal;       // 横軸
            m_d.y = m_joystick.Vertical;         // 縦軸

            // カメラが2D空間上を映す場合
            if (!camera2Dor3DFlag)
            {
                // 2D空間上のプレイヤーの移動速度を取得する
                Vector3 movevellocity = new Vector3(m_d.x, 0.0f, 0.0f);

                // 実際にプレイヤーを動かす
                MoveByDirection(movevellocity);
            }
            // カメラが3D空間上を映す場合
            else
            {
                // 3D空間上のプレイヤーの移動速度を取得する
                Vector3 movevellocity = new Vector3(m_d.y, 0.0f, -m_d.x);

                // 3Dカメラがプレイヤーより手前に引くか決める
                s_camerafollowplayer.Judge3DCameraPlayerBack(new Vector2(-m_d.x,m_d.y));

                // 実際にプレイヤーを動かす
                MoveByDirection(movevellocity);
            }
        }
    }

    /// <summary>
    /// 実際にプレイヤーの速度の向きに合わせて、動かす    
    /// </summary>
    /// <param name="direction">速度の向き</param>
    public void MoveByDirection(Vector3 direction)
    {
        // 何もジョイスティック操作による移動がない場合、何もしない
        if (direction == Vector3.zero) return;

        // 現在プレイヤーが進んでいる速度
        Vector3 playervelocity = m_rigidbody.velocity;

        // 前と今のプレイヤーの速度が正負反対の場合
        // X軸
        if (((oncemovevelocity.x < 0) && (direction.x > 0)) ||
            ((oncemovevelocity.x > 0) && (direction.x < 0)))
        {
            // X軸の速度を反転する
            playervelocity.x *= -1;
        }
        // Z軸
        if (((oncemovevelocity.z < 0) && (direction.z > 0)) ||
            ((oncemovevelocity.z > 0) && (direction.z < 0)))
        {
            // Y軸の速度を反転する
            playervelocity.z *= -1;
        }

        //　反転された速度を反映させる
        m_rigidbody.velocity = playervelocity;

        // プレイヤーが最大速度より超えていない場合
        if (m_rigidbody.velocity.magnitude < m_maxvel)
        {
            if(m_playerType.IsPlayerType == PlayerType.Type.IRON)
            {
                PlayerObj.transform.Translate(new Vector3(direction.z, direction.x, 0) * 0.1f);
            }
            else
            {
                // 3D空間上によるプレイヤーの移動 
                m_rigidbody.AddForce(m_vel * direction, ForceMode.Force);
            }


        }

        // プレイヤーの速度を反転させたり加速させた後、プレイヤーの速度が最大速度より超えた場合
        if (m_rigidbody.velocity.magnitude > m_maxvel)
        {
            // プレイヤーの向きを正規化する
            Vector3 tempmovevellocity = direction;
            tempmovevellocity.Normalize();

            // プレイヤーの最大速度内に収める
            m_rigidbody.velocity = m_maxvel * tempmovevellocity;
        }

        // 前のプレイヤーの速度を取得する
        oncemovevelocity = direction;
    }

    //属性によって速度を変える
    public void ChangeVel()
    {
        m_vel = m_eraserVel + 200.0f;
    }

    //ジョイスティックで動かした方向取得・設定
    public Vector2 D { get { return m_d; } set { m_d = value; } }
}

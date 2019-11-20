//=======================================================================================
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
    //2Dカメラ ↔ 3Dカメラへ動くクラス
    [SerializeField]
    private CameraDirector m_cameradirector = default;

    //イベント管理クラス
    [SerializeField]
    private EventDirector m_event = default;

    //ジョイスティッククラス
    [SerializeField]
    private Joystick m_joystick = default;

    //速度 <目安:100>
    [SerializeField]
    private float m_vel = default;

    // 最大速度<目安:4.0>
    [SerializeField]
    private float m_maxvel = default;

    // プレイヤー
    private GameObject PlayerObj;

    // プレイヤーのRigidbody
    private Rigidbody rigidbody;

    // 前のプレイヤーの速度；
    private Vector3 oncemovevelocity;

    //ジョイスティックで動かした方向X
    private Vector2 m_d;

    // Start is called before the first frame update
    void Start()
    {
        // プレイヤーを探す
        PlayerObj = GameObject.FindGameObjectWithTag("Player");

        // プレイヤーのRigidbodyを探す
        rigidbody = PlayerObj.transform.GetComponent<Rigidbody>();
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
        // 2Dと3Dのカメラの切り替え中フラグ
        bool cameraSwitch2D3D = m_cameradirector.IsMove2D3DCameraPos;

        //2Dと3Dのカメラの切り替え中かどうか,イベントが何も起きていないか
        if ((!cameraSwitch2D3D) && (m_event.IsEventKIND == EventDirector.EventKIND.NONE))
        {
            //ジョイスティックで動かした方向
            m_d.x = m_joystick.Horizontal;       // 横軸
            m_d.y = m_joystick.Vertical;         // 縦軸

       
            //2Dカメラか3Dカメラかのフラグ
            bool camera2Dor3DFlag = m_cameradirector.IsAppearCamera3D;
            
            // カメラが2D空間上を映す場合
            if (!camera2Dor3DFlag)
            {
                // 2D空間上のプレイヤーの移動速度を取得する
                Vector3 movevellocity = new Vector3(m_d.x, 0.0f, 0.0f);

                // 実際にプレイヤーを動かす
                MoveByDirection(movevellocity);
            }
            // カメラが2D空間上を映す場合
            else
            {
                // 3D空間上のプレイヤーの移動速度を取得する
                Vector3 movevellocity = new Vector3(m_d.y, 0.0f, -m_d.x);

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
        Vector3 playervelocity = rigidbody.velocity;

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
        rigidbody.velocity = playervelocity;

        // プレイヤーが最大速度より超えていない場合
        if (rigidbody.velocity.magnitude < m_maxvel)
        {
            // 3D空間上によるプレイヤーの移動 
            rigidbody.AddForce(m_vel * direction, ForceMode.Force);
        }

        // プレイヤーの速度を反転させたり加速させた後、プレイヤーの速度が最大速度より超えた場合
        if (rigidbody.velocity.magnitude > m_maxvel)
        {
            // プレイヤーの向きを正規化する
            Vector3 tempmovevellocity = direction;
            tempmovevellocity.Normalize();

            // プレイヤーの最大速度内に収める
            rigidbody.velocity = m_maxvel * tempmovevellocity;
        }

        // 前のプレイヤーの速度を取得する
        oncemovevelocity = direction;
    }


    //ジョイスティックで動かした方向取得・設定
    public Vector2 D { get { return m_d; } set { m_d = value; } }
}

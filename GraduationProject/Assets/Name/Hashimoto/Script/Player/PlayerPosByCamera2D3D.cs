﻿// -----------------------------------------------------------------------------------------
//! @file       PlayerPosByCamera2D3D.cs
//!
//! @brief      2Dカメラ⇔3Dカメラへ移動時にいるプレイヤーの位置
//!
//! @author     橋本 奉武
//!
//! @date       2019.9.30
// -----------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosByCamera2D3D : MonoBehaviour
{
    // 3Dカメラから2Dカメラに移動する場合のプレイヤーのZ位置
    [SerializeField]
    private float ZPos_CameraFrom3DTo2D = -2.0f;

    // 2Dカメラから3Dカメラに移動する場合のプレイヤーのZ位置
    [SerializeField]
    private float ZPos_CameraFrom2DTo3D = 0.0f;

    // カメラの監督
    [SerializeField]
    private GameObject CameraDirector = default;

    // 2Dカメラ⇔3Dカメラへ移動する前にプレイヤーがいた位置
    private Vector3 OncePos;

    // 2Dカメラ⇔3Dカメラへ移動したときに、プレイヤーに当たった地面以外のオブジェクト
    private GameObject HitNoGroundObj;

    // 2Dカメラ⇔3Dカメラへ移動したときに、プレイヤーが地面以外のオブジェクトに当たったか
    private bool IsHitNoGroundObj;

    /// <summary>
    /// 開始処理
    /// </summary>
    void Start()
    {
        
    }

    /// <summary>
    /// 2Dカメラ⇔3Dカメラへ移動した場合、プレイヤーの位置を変える
    /// </summary>
    /// <param name="IsCamera3D">最終的に3Dカメラになるのか</param>
    public void KeepPlayerPosByCameraMove2D3D(bool IsCamera3D)
    {
        // 2Dカメラから3Dカメラに移動する場合
        if(IsCamera3D==true)
        {
            // プレイヤーの位置を維持する
            transform.position = new Vector3(OncePos.x, OncePos.y, ZPos_CameraFrom2DTo3D);
        }
        else
        // 3Dカメラから2Dカメラに移動する場合
        {
            // プレイヤーの位置を維持する
            transform.position = new Vector3(OncePos.x, OncePos.y, ZPos_CameraFrom3DTo2D);
        }
    }

    /// <summary>
    /// 当たり判定
    /// </summary>
    /// <param name="collision">プレイヤーに当たったオブジェクト</param>
    void OnCollisionEnter(Collision collision)
    {
        // 2Dカメラ⇔3Dカメラに移動している場合
        if(CameraDirector.GetComponent<CameraDirector>().IsMove2D3DCameraPos)
        {
            // --------------------------------------------------------------------------
            // <テスト>

            // 「Ground (2)」に当たった場合
            if(collision.gameObject.name== "Ground (2)")
            {
                // 「プレイヤーが地面以外のオブジェクトに当たった」とする
                IsHitNoGroundObj = true;
            }

            // --------------------------------------------------------------------------
        }
    }

    /// <summary>
    /// 取得・設定関数
    /// </summary>

        // 2Dカメラ⇔3Dカメラへ移動する前にプレイヤーがいた位置
        public Vector3 PlayerOncePos { get { return OncePos; } set { OncePos = value; } }

        // 2Dカメラ⇔3Dカメラへ移動したときに、プレイヤーが地面以外のオブジェクトに当たったか
        public bool IsHitPlayerNoGroundObj { get { return IsHitNoGroundObj; } set { IsHitNoGroundObj = value; } }
}

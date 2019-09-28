// -----------------------------------------------------------------------------------------
//! @file       CameraFollowPlayer.cs
//!
//! @brief      カメラがプレイヤーに追従する
//!
//! @author     橋本 奉武
//!
//! @date       2019.9.28
// -----------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    // 2Dカメラ
    [SerializeField] private GameObject Camera2D = default;

    // 3Dカメラ
    [SerializeField] private GameObject Camera3D = default;

    // 2Dカメラとプレイヤーの距離
    private Vector3 Direction2DCameraPlayerPos;

    // 3Dカメラとプレイヤーの距離
    private Vector3 Direction3DCameraPlayerPos;

    // プレイヤー
    private GameObject Player;

    /// <summary>
    /// 開始処理
    /// </summary>
    void Start()
    {
        // プレイヤーを探す
        Player = GameObject.FindGameObjectWithTag("Player");

        // 2Dカメラとプレイヤーの距離を計る
        Direction2DCameraPlayerPos = Camera2D.transform.position - Player.transform.position;

        // 3Dカメラとプレイヤーの距離を計る
        Direction3DCameraPlayerPos = Camera3D.transform.position - Player.transform.position;
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // 2Dカメラは、常に一定の距離でプレイヤーに追従する
        Camera2D.transform.position = Player.transform.position + Direction2DCameraPlayerPos;

        // 3Dカメラは、常に一定の距離でプレイヤーに追従する
        Camera3D.transform.position = Player.transform.position + Direction3DCameraPlayerPos;
    }
}

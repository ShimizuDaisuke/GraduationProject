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

    // カメラがプレイヤーに追従するのにかかる時間(/s) <カメラのブレを防止するため>
    private float FollowingTime = 5.0f;

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
    void LateUpdate()
    {
        // 2Dカメラは、常に一定の距離でプレイヤーに追従する
        FollowPlayer(Camera2D, Direction2DCameraPlayerPos);

        // 3Dカメラは、常に一定の距離でプレイヤーに追従する
        FollowPlayer(Camera3D, Direction3DCameraPlayerPos);
    }

    /// <summary>
    /// カメラは、常に一定の距離でプレイヤーに追従する
    /// </summary>
    /// <param name="camera">カメラ</param>
    /// <param name="direction">カメラとプレイヤーの距離</param>
    void FollowPlayer(GameObject camera,Vector3 direction)
    {
        // 現在のカメラの位置
        Vector3 NowPos = camera.transform.position;

        // カメラが次へ進む目的地の位置 
        Vector3 NextPos = Player.transform.position + direction;

        // 時間をかけて、カメラはプレイヤーに追従する <カメラのブレ防止>
        camera.transform.position = Vector3.Lerp(NowPos, NextPos, FollowingTime * Time.deltaTime); 
    }
}

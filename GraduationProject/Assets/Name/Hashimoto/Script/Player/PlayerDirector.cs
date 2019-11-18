//=======================================================================================
//! @file   PlayerDirector.cs
//!
//! @brief  プレイヤーの監督
//!
//! @author 橋本奉武
//!
//! @date   11月15日
//!
//! @note  プレイヤーの更新処理はここにまとめる
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDirector : MonoBehaviour
{
    // スクリプト：プレイヤー操作の処理
    private PlayerController Script_PlayerController;

    // スクリプト： Playerを点滅させる処理
    private PlayerFlashing Script_PlayerFlashing;

    /// <summary>
    /// 開始処理
    /// </summary>
    void Start()
    {
        // スクリプト：プレイヤー操作の処理
        Script_PlayerController = GetComponent<PlayerController>();

        // スクリプト： Playerを点滅させる処理
        Script_PlayerFlashing = GetComponent<PlayerFlashing>();

    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // プレイヤーによる操作
        Script_PlayerController.Move();

        // プレイヤーを点滅させ、無敵時間を計る
        Script_PlayerFlashing.FlashingAndUnrivaled();

    }
}

// -----------------------------------------------------------------------------------------
//! @file       Obj_AppearDisPlayerPos.cs
//!
//! @brief      カメラが3D→2Dへ切り替えたときに、
//!             プレイヤーが特定のオブジェクトの上に乗っている場合は特定のオブジェクトの子を表示、
//!             乗っていない場合は子を非表示する処理
//!
//! @author     橋本 奉武
//!
//! @date       2019.10.24
// -----------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obj_AppearDisPlayerPos : MonoBehaviour
{
    // プレイヤーが特定のオブジェクトの上に乗ったか
    private bool IsPlayerRide = false;

    // カメラが3D→2Dへ切り替えたときに、プレイヤーがいる位置によって表示や非表示させるオブジェクト
    [SerializeField]
    private GameObject AppearDisObjByPlayerPos = default;

    /// <summary>
    /// カメラが3D→2Dへ切り替えたときに、プレイヤーがいる位置によって、とあるオブジェクトを表示か非表示させるか決める
    /// </summary>
    /// <param name="IsCamera3D">最終的に3Dカメラになるのか</param>
    public void DecideAppearDisAppearObjByPlayerPos(bool IsCamera3D)
    {
        // 最終的に3Dカメラを写す場合
        if(IsCamera3D == true)
        {
            // とあるオブジェクトを表示させる
            AppearDisObjByPlayerPos.SetActive(true);
        }
        else
        // 最終的に2Dカメラを写す(3D→2D)場合
        {
            // プレイヤーがオブジェクトAの上に乗った場合、オブジェクトBを表示させる(Aに乗っていない場合は、Bを非表示させる)
            AppearDisObjByPlayerPos.SetActive(IsPlayerRide);

            // リセットする
            IsPlayerRide = false;
        }
    }

    /// <summary>
    /// 取得・設定関数
    /// </summary>

    // プレイヤーが特定のオブジェクトの上に乗ったか
    public bool IsPlayerRideOnTheObj {private get { return IsPlayerRide; } set { IsPlayerRide = value; } }

}

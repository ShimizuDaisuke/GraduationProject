// -----------------------------------------------------------------------------------------
//! @file       Change2D3D.cs
//!
//! @brief     2Dカメラと3Dカメラの切り替え
//!
//! @author     橋本 奉武
//!
//! @date       2019.9.26
// -----------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change2D3D : MonoBehaviour
{
    // 2Dカメラ
    [SerializeField]
    private GameObject Camera2D;

    // 3Dカメラ
    [SerializeField]
    private GameObject Camera3D;

    // 3Dカメラを表示するか(false：2Dカメラで表示している / true：3Dカメラで表示している)
    private bool IsNowChange3DCamera = false;

    // 以前3Dカメラを表示させたか(false：2Dカメラで表示した / true：3Dカメラで表示した)
    private bool IsOnceChange3DCamera;

    // 「2Dカメラのみ」もしくは「3Dカメラのみ」に表示されるオブジェクト
    private AppearDisAppearObject Script_AppearDisAppearObjByCamera;

    /// <summary>
    /// 開始処理
    /// </summary>
    void Start()
    {
        // 以前のカメラ状態の設定
        IsOnceChange3DCamera = IsNowChange3DCamera;

        // 2Dカメラの表示の設定
        Camera2D.SetActive(!IsNowChange3DCamera);

        // 3Dカメラの表示の設定
        Camera3D.SetActive(IsNowChange3DCamera);

        // 「2Dカメラのみ」もしくは「3Dカメラのみ」に表示されるオブジェクトの設定
        Script_AppearDisAppearObjByCamera = GetComponent<AppearDisAppearObject>();

    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // <テスト>----------------------------------------------------------------

        // スペースキーを押されたらカメラを切り替える
        if(Input.GetKeyDown(KeyCode.Space))
        {
            IsNowChange3DCamera = !IsNowChange3DCamera;
        }

        // -------------------------------------------------------------------------

        // カメラ状態が変わった場合
        if(IsOnceChange3DCamera!= IsNowChange3DCamera)
        {
            // 2Dカメラの表示の設定
            Camera2D.SetActive(!IsNowChange3DCamera);

            // 3Dカメラの表示の設定
            Camera3D.SetActive(IsNowChange3DCamera);

            // 2Dや3Dカメラのみ表示されるオブジェクトを表示非表示させる
            Script_AppearDisAppearObjByCamera.ChangeObjByCamera(IsNowChange3DCamera);
        }

        // 常に以前のカメラ状態を更新する
        IsOnceChange3DCamera = IsNowChange3DCamera;
    }

    /// <summary>
    /// 取得・設定関数
    /// </summary>
    
        // 3Dカメラを表示するか(false：2Dカメラ表示 / true：3Dカメラ表示)
        public bool Change3DCamera { get { return IsNowChange3DCamera; } private set { IsNowChange3DCamera = value; } }
}

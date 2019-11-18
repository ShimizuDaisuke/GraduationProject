//=======================================================================================
//! @file   EventCameraPencilcaseMoveGool.cs
//! @brief  筆箱に入る時のカメラの処理
//! @author 田中歩夢
//! @date   11月18日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//筆箱に入る時のカメラの処理のクラス
public class EventCameraPencilcaseMoveGool : CameraEventBase
{
    //ターゲットオブジェクト
    [SerializeField]
    private GameObject m_targetObj;

    void Awake()
    {
        // 初期化する
        Initilaize();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    /// <summary>
    /// イベント用にカメラを動かす
    /// </summary>
    public override void MoveCameraByEvent()
    {
        Camera3D.transform.position = m_targetObj.transform.position;
        Camera3D.transform.rotation = m_targetObj.transform.rotation;
    }
}

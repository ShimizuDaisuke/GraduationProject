//=======================================================================================
//! @file   EventCameraRuleStraight.cs
//! @brief  投げる前のカメラの処理
//! @author 田中歩夢
//! @date   10月25日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//投げる前のカメラの動きのクラス
public class EventCameraRuleStraight : CameraEventBase
{
    //定規のオブジェクト
    [SerializeField]
    private GameObject m_seesawObj;

    // Start is called before the first frame update
    void Start()
    {
        // 初期化する
        Initilaize();
    }


    /// <summary>
    /// イベント用にカメラを動かす
    /// </summary>
    public override void MoveCameraByEvent()
    {
        //Camera3D.transform.RotateAround(m_seesawObj.transform.position,Vector3.up, 5.0f);
        //Camera3D.transform.rotation = Quaternion.RotateTowards(Camera3D.transform.rotation, Quaternion.Euler(/*m_seesawObj.transform.localEulerAngles + */new Vector3(0.0f,180.0f,0.0f)), 2.0f);
        //Debug.Log("fdasgafuyfa");
    }
}
